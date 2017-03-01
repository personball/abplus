using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.IO;
using System.Web;
using Abp.Dependency;
using Abp.Extensions;
using Abp.UI;

namespace Abp.Web.SimpleCaptcha
{
    public class SimpleCaptchaManager : ITransientDependency
    {
        public const string DefaultCharSetNumbers = "0123456789";
        public const string DefaultCharSetUpperCases = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
        public const string DefaultCharSetLowerCases = "abcdefghijklmnopqrstuvwxyz";

        private readonly IVerificationCodeStore _codeStore;
        private readonly ISimpleCaptchaModuleConfig _config;

        public SimpleCaptchaManager(
            IVerificationCodeStore codeStore,
            ISimpleCaptchaModuleConfig config
            )
        {
            _codeStore = codeStore;
            _config = config;
        }

        public virtual void DisableHttpResponseCache()
        {
            //禁止图片缓存 
            HttpContext.Current.Response.Cache.SetCacheability(HttpCacheability.NoCache);
        }

        public virtual void ClearHttpResponseAndSetContentType()
        {
            HttpContext.Current.Response.Clear();
            HttpContext.Current.Response.ContentType = "image/pjpeg";
        }

        /// <summary>
        /// 输出验证码图片
        /// </summary>
        /// <param name="storeKey">验证码字串存储的键名</param>
        /// <param name="ImgWidth">图片宽，必须大于CodeNum*17</param>
        /// <param name="ImgHeight">图片高，必须大于20</param>
        /// <param name="CodeNum">字符数量</param>
        public void ImageRefresher(Stream outputStream, string storeKey, int ImgWidth, int ImgHeight, int CodeNum)
        {
            if (ImgWidth <= CodeNum * 17)
            {
                ImgWidth = CodeNum * 17 + 1;
            }

            if (ImgHeight <= 20)
            {
                ImgHeight = 21;
            }

            DisableHttpResponseCache();

            //设置字符集
            string charSetString = string.Empty;//增大字符集
            if (_config.CharSetIncludeNumbers)
            {
                charSetString += DefaultCharSetNumbers;
            }

            if (_config.CharSetIncludeLowercases)
            {
                charSetString += DefaultCharSetLowerCases;
            }

            if (_config.CharSetIncludeUppercases)
            {
                charSetString += DefaultCharSetUpperCases;
            }

            if (!_config.CharSetExcluded.IsNullOrWhiteSpace())
            {
                var units = _config.CharSetExcluded.ToCharArray();
                foreach (var unit in units)
                {
                    charSetString.Replace(unit.ToString(), "");
                }
            }

            int ccslength = charSetString.Length;

            //设置字体集合
            Font[] fonts = new Font[] {
                new Font("宋体", 15, FontStyle.Bold|FontStyle.Regular),
                new Font("宋体", 16, FontStyle.Bold|FontStyle.Italic),
                new Font("宋体", 17, FontStyle.Bold|FontStyle.Strikeout),
                new Font("宋体", 16, FontStyle.Bold|FontStyle.Underline),
                new Font("宋体", 15, FontStyle.Bold|FontStyle.Italic|FontStyle.Strikeout),
                new Font("宋体", 16, FontStyle.Bold|FontStyle.Italic|FontStyle.Underline)
            };

            //设置画笔
            Brush brush = null;
            Color brushColor = new Color();

            //验证码字串
            string valiCode = string.Empty;
            string codeUnit = string.Empty;

            Bitmap image = new Bitmap(ImgWidth, ImgHeight);//图片规格
            //范围
            int yOffset = (image.Height - 17) / 2;//字符垂直偏移度
            int[] offSetFixArr = new int[] { -4, -3, -2, -1, 0, 1, 2, 3, 4 };

            Graphics g = Graphics.FromImage(image);//画布
            g.Clear(Color.White);//初始化画布

            int startOffset = (image.Width - CodeNum * 17) / 2;
            int startX = startOffset;
            Random random = new Random();
            string tmpcode = "";
            for (int i = 0; i < CodeNum; i++)
            {
                int rNum1 = random.Next(ccslength, DateTime.Now.Millisecond + ccslength);
                int rNum2 = random.Next(fonts.Length, DateTime.Now.Millisecond + fonts.Length);
                int rNum3 = random.Next(offSetFixArr.Length, DateTime.Now.Millisecond + offSetFixArr.Length);

                codeUnit = charSetString.Substring(rNum1 % ccslength, 1);//随机提取字符
                if (codeUnit == tmpcode)
                {
                    codeUnit = charSetString.Substring((rNum1 + 1) % ccslength, 1);//与上一字符重复时，再次提取字符
                }

                valiCode = valiCode + codeUnit;//记录提取的字符
                tmpcode = codeUnit;

                //调整画笔以及写入坐标，实现验证码粘连、倾斜
                Font font = fonts[rNum2 % fonts.Length];//选择字体
                brushColor = Color.FromArgb(random.Next(255), random.Next(80), random.Next(255));//设置随机颜色
                brush = new SolidBrush(brushColor);
                int offSetFix = offSetFixArr[rNum3 % offSetFixArr.Length];

                g.DrawString(codeUnit, font, brush, startX, yOffset + offSetFix);
                startX += 13 + offSetFix;
            }

            //随机产生的线条
            if (_config.RandomLineEnabled)
            {
                LinearGradientBrush lgb = new LinearGradientBrush(new Rectangle(0, 0, image.Width, image.Height), Color.Blue, Color.Red, 1.2f, true);
                for (int i = 0; i < _config.RandomLineCount; i++)
                {
                    int x1 = random.Next(image.Width);
                    int x2 = random.Next(image.Width);
                    int y1 = random.Next(image.Height);
                    int y2 = random.Next(image.Height);
                    g.DrawLine(new Pen(Color.Black), x1, y1, x2, y2);
                }
            }

            //背景随机点
            for (int i = 0; i < 100; i++)
            {
                int x1 = random.Next(image.Width);
                int y1 = random.Next(image.Height);
                image.SetPixel(x1, y1, Color.FromArgb(random.Next(255), random.Next(255), random.Next(255)));
            }

            int rNum4 = random.Next(0, 6);
            //扭曲
            if (_config.TwistEnabled)
            {
                image = TwistImage(image, true, 3, rNum4);
            }

            ClearHttpResponseAndSetContentType();

            image.Save(outputStream, ImageFormat.Jpeg);
            _codeStore.Save(storeKey, valiCode);

            image.Dispose();
        }

        #region 产生波形滤镜效果

        private const double PI = 3.1415926535897932384626433832795;
        private const double PI2 = 6.283185307179586476925286766559;

        /// <summary>
        /// 正弦曲线Wave扭曲图片
        /// </summary>
        /// <param name="srcBmp">图片路径</param>
        /// <param name="bXDir">如果扭曲则选择为True</param>
        /// <param name="dMultValue">波形的幅度倍数，越大扭曲的程度越高，一般为3</param>
        /// <param name="dPhase">波形的起始相位，取值区间[0-2*PI)</param>
        /// <returns></returns>
        private Bitmap TwistImage(Bitmap srcBmp, bool bXDir, double dMultValue, double dPhase)
        {
            Bitmap destBmp = new Bitmap(srcBmp.Width, srcBmp.Height);

            // 将位图背景填充为白色
            Graphics graph = Graphics.FromImage(destBmp);
            graph.FillRectangle(new SolidBrush(Color.White), 0, 0, destBmp.Width, destBmp.Height);

            double dBaseAxisLen = bXDir ? (double)destBmp.Height : (double)destBmp.Width;

            for (int i = 0; i < destBmp.Width; i++)
            {
                for (int j = 0; j < destBmp.Height; j++)
                {
                    double dx = 0;
                    dx = bXDir ? (PI2 * (double)j) / dBaseAxisLen : (PI2 * (double)i) / dBaseAxisLen;
                    dx += dPhase;
                    double dy = Math.Sin(dx);

                    // 取得当前点的颜色
                    int nOldX = 0, nOldY = 0;
                    nOldX = bXDir ? i + (int)(dy * dMultValue) : i;
                    nOldY = bXDir ? j : j + (int)(dy * dMultValue);

                    Color color = srcBmp.GetPixel(i, j);
                    if (nOldX >= 0 && nOldX < destBmp.Width
                     && nOldY >= 0 && nOldY < destBmp.Height)
                    {
                        destBmp.SetPixel(nOldX, nOldY, color);
                    }
                }
            }

            graph.DrawRectangle(new Pen(Color.Silver), 0, 0, destBmp.Width - 1, destBmp.Height - 1);
            graph.Dispose();
            return destBmp;
        }
        #endregion

        public bool Verify(string storeKey, string codeToBeVerified)
        {
            var codeStored = _codeStore.Find(storeKey);
            if (codeStored.IsNullOrWhiteSpace())
            {
                throw new UserFriendlyException("VerificationCode Expired!");
            }

            var matched = false;
            if (_config.CaseSensitive)
            {
                matched = (codeStored == codeToBeVerified);
            }
            else
            {
                matched = (codeStored.ToLower() == codeToBeVerified.ToLower());
            }

            if (matched && !_config.CodeReusable)
            {
                _codeStore.Clear(storeKey);
            }

            return matched;
        }
    }
}
