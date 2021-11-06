# WRSoftware.Utils.Notifications
## With colaboration of [Carlos Gonçalves](https://cgoncalves.com/)
# How to use?

### Inject the Notification Instance:
```csharp
services.AddScoped<NotificationContext>();
```

### Add the Notification Filter:
```csharp
services.AddMvc(options => options.Filters.Add<NotificationFilter>());
```

### Example of how to Use:
```csharp
public class MyClass
{
	private readonly NotificationContext _notificationContext;

	public MyClass(NotificationContext notificationContext)
	{
	    _notificationContext = notificationContext;
	}

	public async Task DoSomething(string name)
	{
	    // verify if exists some any notification
	    if (_notificationContext.HasErrors)
	    {
	        return null;
	    }

	   // Do anything to you code
	    if (response.StatusCode == HttpStatusCode.Conflict)
	    {
	        // add a message error
	        _notificationContext.Add(new Critical("Error", "Already exists an account with that e-mail address."));
	        return null;
	    }
	}
}
```
### Class
- #### Critical
	This error type is detect by the filter, if exists any error with that type a 400 is returned.
- #### Information
- #### Warning

### Methods
#### Notification Manipulation

 - #### Add
	 Add a notification of the desired type.	
	 ```csharp
	  _notificationContext.Add(new Critical("Key", "Message"))
	  _notificationContext.Add(new Information("Key", "Message"))
	  _notificationContext.Add(new Warning("Key", "Message"))
	```
	
-	#### ClearNotifications
	Clear all the notifications:
	```csharp
	  _notificationContext.ClearNotifications()
	  ```
	 Clear notifications by the type:
	```csharp
	  _notificationContext.ClearNotifications(typeof(Critical))
	  _notificationContext.ClearNotifications(typeof(Information))	
	  _notificationContext.ClearNotifications(typeof(Warning))	
	  ```
#### Verification of notifications

- #### HasNotifications (property)
	Returns a boolean that will indicate is exists notification of any type.
	```csharp
	  var hasNotifications = _notificationContext.HasNotifications
	```

- #### HasErrors (property)
	Returns a boolean that will indicate is exists notification of type **Critical**.
	```csharp
	  var hasNotifications = _notificationContext.HasErrors
	```

- #### HasWarnings (property)
	Returns a boolean that will indicate is exists notification of type **Warning**.
	```csharp
	  var hasNotifications = _notificationContext.HasWarnings
	```

- #### HasInformations (property)
	Returns a boolean that will indicate is exists notification of type **Information**.
	```csharp
	  var hasNotifications = _notificationContext.HasInformations 
	```

#### Acessing the notifications

- #### Notifications (property)
	Returns a list with all the objects of type **Error**, the class where all the notications types inherited from. With this property is possible to get all the notications type tha exists.
	```csharp
	  var notifications = _notificationContext.Notifications
	```
	
- #### Errors 
	Returns a list of objects with the type **Critical**.
	```csharp
	  var notifications = _notificationContext.Errors
	```
	
- #### Warnings
	Returns a list of objects with the type **Warning**.
	```csharp
	  var notifications = _notificationContext.Warnings
	```
	
- #### Informations 
	Returns a list of objects with the type **Information**.
	```csharp
	  var notifications = _notificationContext.Informations
	```