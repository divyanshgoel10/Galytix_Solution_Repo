Name: Divyansh Goel, Enroll: 20103174

Running the Application: Setup Environment:

Make sure you have Visual Studio installed (VS2019, VS2021, or VS2022). Ensure that the required NuGet packages are restored. You can do this by building the solution. Configure CSV Data:

Ensure that the gwpByCountry.csv file is present in the Data folder of your project. Run the Application:

Open the solution in Visual Studio. Set the Galytix.WebApi project as the startup project. Press Ctrl + F5 or click on the "Start" button to build and run the application. Access the API:

Once the application is running, you can access the API at http://localhost:9091/api/gwp/avg using tools like Postman or curl. Testing the API: Test with Postman:

Open Postman and create a new POST request.

Set the request URL to http://localhost:9091/api/gwp/avg.

Set the request body to the following JSON format:

json Copy code { "country": "ae", "lob": ["property", "transport"] } Send the request and verify the response.
