# Notes  

### The following is an overview of the project  

Going to write this portion later  

# Setup  

### The following is a detailed description of how to add to this app  


#### UI  

1. The actual UI that you see is pretty straightforward to setup. Go into the canvas and duplicate an existing View container to make things simple. Just don't use start or results. This will give you some default things such as a close button, header, message, play button, etc.. Remember that all these things are visible in the editor, but won't just work without registering the new views, buttons, etc which will be explained further down.  

2. Make sure to rename the game objects, and swap out images. For example. If you duplicated ColdShowerViews, you'd want to rename everything from "ColdShower" to "EXAMPLENAMEHERE" in every respective game object.  

#### Setup Overview

1. The app uses MVC architecture for it's UI's as well as a Registry for keeping track of different object references. The following will give a high level overview of how to get started with this architecture. One thing to mention that will speed the process up, is if you look at other Classes within similar folders to the ones mentioned below, you'll see that they all do very specific things, inherit from specific base classes or interfaces, and their "Skeleton" can be easily copied into new classes to speed things up. By this, I mean, it would be easiest to copy the library imports, folder naming, class naming conventions, namespacing, basic Unity functions, etc.    


##### Enums

1. Open Enums.cs and add new `ViewName` and `SavedResultType` values. For example, with a Mediation module, we could add `MeditationStart, & MeditationCompletion`, as well as a `MeditationMonthlyStatistics, & MeditationSpecifics` to the ViewName Enums. (*Don't worry about the last 2, those are defaults that any new module will need to keep track of progress, just make sure to add them) Additionally, you'll want to add a "Meditation" value to the SavedResultType enums. One thing to note is that with Enums, you never want to insert new values into the Enum, you want to append them to the end of it. Otherwise, this will cause some issues.  


##### Folders  

1. If you browse to `Assets/Scripts/UI/Views` or `Assets/Scripts/UI/Controllers`  you'll want to add a respective folder for any new module. Meditation, for example, would need a folder within each folder mentioned.  


##### Controllers classes  

1. if you created a meditation module, for example, then within the `Assets/Scripts/UI/Controllers/Meditation` folder, you'll want to create a class named after the `ViewName`'(s)  you created in the Enum file. This approach loosely follows a standard naming convention in MVC. For example, there would be a `MeditationStartController", & "MeditationCompletionController`.   

2. The other 2, `MeditationMonthlyStatisticsController, & MeditationSpecificsController`, would go within the `Assets/Scripts/UI/Views/Results/` folder. Make sure to append the word "Controller" to any view created to give it context within the application.  

3. You'll also want a `MeditationController` which will be a singleton that each respective Meditation controller would be able to "talk" to. So far, most of these master controllers don't do much other than update models in a convenient location.  

4. Completion Controller - Double check another completion controller's source code to see what I mean here, but make sure the completion controller's all save data in the proper way. It will be outlined further below.  

5. Make sure the classes inherit from `Controller`  


##### View Classes  

1. if you created a meditation module, for example, then within the `Assets/Scripts/UI/Views/Meditation` folder, you'll want to create a class named after the `ViewName`'(s)  you created in the Enum file. For example, there would be a `MeditationStartView", & "MeditationCompletionView`. 

2. The other 2, `MeditationMonthlyStatisticsView, & MeditationSpecificsView`, would go within the `Assets/Scripts/UI/Views/Results/` folder. Make sure to append the word "View" to any view created to give it context within the application.  

3. Make sure they inherit from `View`  


##### Model classes  

1. If you created a meditation module, you'd want to add a `MeditationModel` class to the `Assets/Scripts/UI/Models` folder  

2. Make sure they inherit from the `Model` base class.  

##### Registry & UI wiring

1. Remember in the UI portion of this documentation when we created the UI? Well we'll want to add our newly created View & controller classes, to those respective views. The "MeditationStartView" gameobject, for example, would get an instance of `MeditationStartView & MeditationStartController` added to it. Make sure to set the appropriate `ViewName` in the view object. You'll see it when you add it.  If a public field named ViewName isn't showing up, then make sure you inherit from the `View` base class inside your view files. This is an important step mentioned above.  

2. The ViewRegistry, is a Registry that all new views need to be added to. All you need to know about this registry is that it's in the `Scripts` game object in scene, and you just drag and drop newly added view game objects into it. DO NOT forget this step, otherwise, newly added views will not be able to be activated, and deactivated from within any controller, or button. The gist of the ViewRegistry, is simple. Views will have an enum value associated with them, that keeps the association between Views, Models, Controllers, etc. This accomplishes something simple but straightforward. Any controller can call `UIController.instance.Open(ViewName);` to open a view, or `UIController.instance.Close(ViewName);` to close a view. There's a few other useful functions in there as well.  `UIController.instance.OpenImmediately(ViewName)`, for example, is used for opening Modals, or views that you want to open over-top of another view without closing the original view you started from. UIController accomplishes this by making calls to the ViewRegistry. Which is essentially just an abstracted `Dictionary<ViewName, View>` with helper functions.  

3. Buttons - There's going to be buttons in your views. Make sure their events are wired up with corresponding functions inside your controllers. For example, "StartButton" may have a "StartButtonClick" function it will be wired up to in its repective controller.  

4. Results Views - Remember those views we discussed above about monthly statistics, and specifics. Those have their own section below to go over how to set them up because they require a bit different setup than normal views in the application.  

5. Home Screen - Add a button to access a newly created `Start` view, and wire it up to a function within `Assets/Scripts/Controllers/StartController.cs` file.  There are plenty of examples in that file that demonstrate this. The function would likely have code in it that looks like the following - `UIController.instance.Open(ViewName.MeditationStart);`. 

6. Within that `Scripts` game object in the main scene, make sure to also add the `MeditationController` or newly created master controller within the UIController gameobject, and the newly created model in the model game object. Make sure to drag and drop the newly created model to the master controller. This is important.  


#####  Testing

If everything has been setup correctly by this step. Classes were created, added to proper folders, inherited from the right classes, have proper importing, were added to the proper game objects, member variables were initialized properly, and everything was drag/dropped as mentioned. Then when you go to the home scene and click on the new module button you created, it should open the start screen to that module. From this point, anything that start screen, or corresponding screens do, will be contained within their respective controllers, & is entirely up to you how they look & behave. They can have similar functionality to any previous module, or they can have completely new functionality and features.  


#####  Results Views  


#####  Save / Load Data  
