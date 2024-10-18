# Email Application

<!-- ABOUT THE PROJECT -->
## About The Project

This project creates a simple email sending system using C# and targeting .Net 8.0.  It consists of four subprojects: an email sending class library (DLL), a console application to test that DLL, an API that implements the DLL, and a frontend to test that API.  The credentials of the email sender can be manually changed in the console applications appsettings.json or in the API's appsettings.json depending on what is being tested.  The URL that the frontend posts to can be changed in its appsettings.json file in case the API is being hosted on another page.

### Four Components:
- **Email Sending Class Library**
  - *Email Class*: Stores components of an email message
  - *Email Credentials Class*: Stores credentials for the email sender
  - *Email Service Class*: Uses Mailkit to send emails
- **Console Application**: Tests the email sending service from the class library
- **Email Sending API**: Provides RESTful access to the email sending service
- **Email Sending Web Form**: Calls on the API to send an email message

## NuGet Packages Used
- Mailkit
- Microsoft.Extensions.Configuration
- Microsoft.Extensions.Configuration.Binder
- Microsoft.Extensions.Configuration.Json
- Swashbuckle.Aspnetcore
- System.Runtime.Serialization.Json
