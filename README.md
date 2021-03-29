# LoginWpfWcf
A simple login application with wpf frontend (client) and wcf backend (server)

## General Description:
“Login” is a C# application with standard login functionality. The solution is based on a Client/Server architecture. The client is the login system that has all the front-end responsibilities. The server is the back end of the application and is responsible for managing the usernames and passwords.

## Technology Stack:

* Frontend: Windows Presentation Foundation (WPF)
* Backend: Windows Communication Foundation (WCF)

## Client/Frontend (WPF Project)
The UI logic is implemented on the client-side of the project. It includes:
1. Login panel
2. Popups for showing messages
3. Admin panel

Moreover, once the login form is filled by the user, login credentials will be sent over to the server for verification.

In general, there are two groups of users:

1. Normal Users (Username != admin): Upon successful login, a simple pop-up window displays with “Successful Login. Welcome {UserName}!” message.
 
2. Admin User (Username == admin): Upon successful login, a simple pop-up window displays with “Successful Login. Welcome admin!” message. 
 
Then another window will be displayed through which the admin can see all user accounts and have an option to delete accounts. In order to get the list of users, the client is communicating with the server and ask for the list of all registered users.

 If admin selects some users and hit ‘Delete Users’:
1. The list of users to delete will be sent to the server 
2. Server will search for these users in the users’ list (credentials.csv) and delete them
3. A pop-up will appear displaying how many users were deleted.
4. UI will be updated with the updated list of registered users.
 
## Server/Backend(WCF Project)
On the server-side, Windows Communication Foundation (WCF) framework was used for building service-oriented applications. Using WCF, endpoint communications can be performed using several built-in transport protocols and encodings. The used protocol in this solution is sending text encoded SOAP messages using the HyperText Transfer Protocol (HTTP) for use on the World Wide Web. Alternatively, WCF allows you to send messages over TCP, named pipes, or MSMQ. 
The server project was configured to be hosted on localhost and port number 57143. 
 
Server is responsible for making all the backend logic of the solution. This includes:
1.	Authentication/Authorization of Normal user: see method “MakeLoginUserNormal“.
2.	Authentication/Authorization of Admin user: see method “MakeLoginUserAdmin“.
3.	Retrieve the list of all registered users and return this list to the client for display in the UI: see method “GetAllUsers“.
4.	Delete a set of users obtained from the client-side (when logged in as admin): see method “DeleteUsers“.

An assumption made here is that all the credentials are saved in a csv file called ‘credentials.csv’. You can find this file under the directory ‘Logic/Server/credentials.csv’. If you want to add a new user to the registry, simply add a new line to this file with the format “username,password”.
In order to secure the passwords, first of all, a password box is used in the UI. This ensures that the password is not visible at the moment of typing.

For sending password from client to server for verification:
1.	On the client-side, it is getting encoded: see method “EncodePasswordToBase64“.
2.	On the server-side, it is getting decoded: see method “DecodeFrom64“.

## How to run the solution?
In visual studio:

Step1 (Run Server): right-click on “Server” project and click on “View in Browser”!
 

Step 2(Run  Client): Start the “Client” project!
 

