You can start the application by double click in the "TrivagoFinance" folder then, 
click on "TrivagoFinance.sln" to be opened with visual studio 2019/2017 -Note run those programs as administrator.
Then double click on the folder with name "4.TrivagoFinanance.UI", 
Then right click on "TrivagoFinance.Ui" and click select it as "StartUp project".
The Connection String for connectng the app with SQL Database is found inside "appsettings.json" file there you will find comment where to change the values 
Small gude from offical documentation https://docs.microsoft.com/en-us/ef/core/miscellaneous/connection-strings under ASP.NET Core.
After that open the Package Manager Console and select  "Default Project: 4.TrivagoFinanace.Ui\TrivagoFinance.UI from the dropdown" 
and type "Update-Database". You can find Package Manager console follow this steps "Tools > NuGet Package Manager > Package Manager Console".
Final Step Click on IIS Express button with the green play icon in it. If you wanna run it in specific browser click  on the dropdown small icon in the same button.
And go Web Browser, select your favorite browser and click the IIS Express button again .
The app will be run by the Code first approach.


IMPORTANT - to run the project!
This project was tested in Windows working environment i have not tested it inside Mac or Linux there for i cannot confirm for his status but the .Net Core can be run on those environments as well, it is cross platform. 
You need to have Visual studio installed 2017 or higher, follow the link
https://visualstudio.microsoft.com/downloads/ to install it, Community version will be just fine and then install 
.NET Core 2.2 SDK https://dotnet.microsoft.com/download, I’m using 2.2.301 version at the time of this guide its the latest stable version 
and it is required to run the project .
Final step to have Sql server, I’m using this version https://www.microsoft.com/en-us/sql-server/sql-server-downloads Developer edition you can use any of those versions, will work just fine.
To view the Data from the SQL you need to have SQL Server Management Studio (SSMS) you can download it here https://docs.microsoft.com/en-us/sql/ssms/download-sql-server-management-studio-ssms?view=sql-server-2017. 
