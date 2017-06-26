"..\.nuget\NuGet.exe" "pack" "..\src\Abplus\Abplus.csproj" -Properties Configuration=Release -IncludeReferencedProjects
"..\.nuget\NuGet.exe" "pack" "..\src\Abplus.Common\Abplus.Common.csproj" -Properties Configuration=Release -IncludeReferencedProjects
"..\.nuget\NuGet.exe" "pack" "..\src\Abplus.Events.Producer\Abplus.Events.Producer.csproj" -Properties Configuration=Release -IncludeReferencedProjects
"..\.nuget\NuGet.exe" "pack" "..\src\Abplus.RebusRabbitmqConsumer\Abplus.RebusRabbitmqConsumer.csproj" -Properties Configuration=Release -IncludeReferencedProjects
"..\.nuget\NuGet.exe" "pack" "..\src\Abplus.RebusRabbitmqProducer\Abplus.RebusRabbitmqProducer.csproj" -Properties Configuration=Release -IncludeReferencedProjects
"..\.nuget\NuGet.exe" "pack" "..\src\Abplus.Web.Api\Abplus.Web.Api.csproj" -Properties Configuration=Release -IncludeReferencedProjects
"..\.nuget\NuGet.exe" "pack" "..\src\Abplus.Web.SignalR\Abplus.Web.SignalR.csproj" -Properties Configuration=Release -IncludeReferencedProjects
"..\.nuget\NuGet.exe" "pack" "..\src\Abplus.Web.SimpleCaptcha\Abplus.Web.SimpleCaptcha.csproj" -Properties Configuration=Release -IncludeReferencedProjects
"..\.nuget\NuGet.exe" "pack" "..\src\Abplus.WebApiClient\Abplus.WebApiClient.csproj" -Properties Configuration=Release -IncludeReferencedProjects
"..\.nuget\NuGet.exe" "pack" "..\src\Abplus.MqMessages\Abplus.MqMessages.csproj" -Properties Configuration=Release -IncludeReferencedProjects
"..\.nuget\NuGet.exe" "pack" "..\src\Abplus.MqMessages.RebusConsumer\Abplus.MqMessages.RebusRabbitMqConsumer.csproj" -Properties Configuration=Release -IncludeReferencedProjects
"..\.nuget\NuGet.exe" "pack" "..\src\Abplus.MqMessages.RebusPublisher\Abplus.MqMessages.RebusRabbitMqPublisher.csproj" -Properties Configuration=Release -IncludeReferencedProjects
"..\.nuget\NuGet.exe" "pack" "..\src\Abplus.EntityFramework\Abplus.EntityFramework.csproj" -Properties Configuration=Release -IncludeReferencedProjects

pause