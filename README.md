# SignalRWebApiWithWpfClient

This is an Ecample for Testing SignalR between a WebAPI and WPF Application
This is an Solution With two Projects
Project 1 : SignalRWebApi		 : DotNet Core WebApi thar Includes SignalR
Project 2 : WpfSignalRClientApp	 : DotNet Core WPF Application that is set up as a SignalR client

When the http:/localhost/Time Get is called, it will also Push a Message to all clients via SignalR
On the WPF application (If Connected) the message will be displayed. 
you can also send a message from the wpf application using the SendMessage Button


THis demo currently only sends string messages.
The next changes will be to send more complex/custom dataTypes
