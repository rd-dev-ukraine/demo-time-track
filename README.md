# LanceTrack time-tracking application.

Demo application developed by R&amp;D Ukraine.

Running application could be found at http://demoapp.rd-dev.net/

Use **demo/demo** login/password to log-in.

Application allows users to track hours per set of projects and then build invoices.
Application uses CQRS arhitechture and uses own CQRS framework.


## How to run:

Application is written with Visual Studio 2013. Also you will need to update TypeScript to version 1.4 or above. Application uses LESS for developing stylesheets. We used WebEssentails 2013 but you could use your favorite LESS compiler.

1. Create database. There is project LanceTrack.Database in solution you need to publish to your SQL Server. By default application is configured to use default SQL Server instance and database LanceTrack. 
2. After publishing database you need to create structure for ELMAH by running ELMAH-db-1.2.sql script.
3. Also you could create demo data by running DemoData.sql script.
4. Build project and run LanceTrack.Web project.
