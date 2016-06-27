# context
This is the repo of the Context group for the Health Informatics context project, TU Delft 2016

Our group name is ContExt, our members are: 

    Luke Prananta		    4288386	    lukeprananta@hotmail.com

    Jasper van Esveld	    4372581	    jaspervanesveld@gmail.com

    Arjan van Schendel	    4366212	    Arjanvanschendel@gmail.com 

    Matthias Tavasszy	    4368401	    m.h.tavasszy@student.tudelft.nl

    Bart Ziengs		        4391799 	a.h.ziengs@student.tudelft.nl

 To open our project in Unity, simply select this folder when in the open project dialog of Unity. Make sure the scene SuperMarket is opened (Assets/Scenes/SuperMarket.unity).
 Run the scene by pressing ctrl+P. Walking around can be done using WASD, looking around using the mouse, jumping by pressing the spacebar.
 The arms are controlled by the LeapMotion (SDK has to be installed), the separate fingers, and thus the grabbing motion, by the ManusVR (SDK included on GitHub).
 
To run the tests we made, you can open the Editor Tests Runner view, found under the Window tab. Coverage cannot be found using Unity, so we created a coverage document manually.
It can be found under SE_Deliverables/Coverage.pdf

 We use the following tools as recommended by CleVR: StyleCop, CodeMaid, GhostDoc and VSSpellingChecker. We use all these tools to improve our final product quality and increase productivity.
StyleCop is used to make sure all of our code has the same style. GhostDoc can automatically generate documentation which makes documenting our code faster and easier. 
To prevent spelling errors we use VSSpellingChecker, and CodeMaid makes it easier to run StyleCop on our code.

 We also make use of Unity Cloud Build which is an easy to use continuous integration solution for Unity projects.
To see our builds and wether or not they succeeded please create a Unity id and send us the email adress that was used so we can add you to the project.
When added the Unity Cloud Build project can be viewed at https://build.cloud.unity3d.com/orgs/tentacola/projects/context/

# Make your own scene
Basic scene and required software <br/>
1. Install the leap motion Orion beta software (https://developer.leapmotion.com/get-started)<br/>
2. Install the Kinect (v1) SDK  (https://www.microsoft.com/en-us/download/details.aspx?id=40278)<br/>
3. Open our project <br/>
4. Check if the build settings are set to the correct platform (Pc, Mac & Linux) <br/>
5. Create a new scene <br/>
6. Copy the player from one of the example scenes <br/>
7. Done! <br/> <br/>

When the ManusVR does not work use the test application from the Manus GitHub (https://github.com/ManusVR/Manus/releases).
If the test application does not show up in the command window then the gloves are not connected.
 <br/> <br/>
Adding interactable objects <br/>
1. Add a collider to the object <br/>
2. Add a rigid body to the object <br/>
3. Set the layer of the object to "interactable" <br/>
4. Done! <br/> <br/>

For a more in depth view on how the player and the grabbing mechanics work watch the following tutorials: <br/>
1. Kinect: https://www.youtube.com/watch?v=yHdZ_dN7qGo <br/>
2. Leap Motion: https://www.youtube.com/watch?v=X9ASfMVcNs4 <br/>
3. ManusVR: https://www.youtube.com/watch?v=FzwLuo1wUiU <br/>
4. Grabbing: https://www.youtube.com/watch?v=nY-XHlRODMg <br/>
