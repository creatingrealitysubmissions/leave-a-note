# noted
### by *team leave-a-note*



## Inspiration

Public communication among people is ruled by social media, making us more isolated in our daily lives. Noted is an app experience using Augmented Reality and GPS location that allows you to leave a note of anything on your mind in a public space, outside, for universal access to everyone. By combining the digital and physical world, we hope to build a new city of thoughts and ideas every time we step out the door!

## What it does

Noted opens public spaces by transforming any space into virtual bulletin board through the use augmented reality.  Anyone can leave note in any physical space to put a message out to world.   

## Challenges

Unity3D is an ever evolving platform for building advanced 3D applications supporting a diverse array of technologies which each have their own complex sets dependencies needed build the modern augmented, virtual and mixed reality experiences.  In order to narrow down the scope the project and focus development to a minimum viable product with the greatest reach it was decided target Apple’s mobile iOS platform and ARkit capable devices first.  This common platform from allowed from accelerated development and prototyping  among the developers but nature of the platform also limited immediate collaboration as the technologies require a significant considerations for all uses to be properly licensed and signed for by a legal person.  Once these obstacles were over come the next technical challenge was establish what functions of our solution could be achieved given the time and resources available.  As there was no readily apparent need for mapping geospacial information data for our application it was opted to pass up on the use of the beta of ArcGIS runtime offered by ESRI.  After some research it was decided  that use of ESRI tech should be explored only after a core working prototype had been built and tested.

The next set of technical challenges were all mostly related to limitations of the tech itself.  As Unity is an advanced 3d game engine supporting a wide range of tools to allow artist and developers to more readily collaborate with each there are a some non trivial considerations to be made when working with git for the purposes of this hackathon.  As git is a decentralized version control system built for developers to solve the needs to developers working with repositories of mostly text sources, it important that binary data such as that used in unity assets for a game is appropriately handled for to improve performance and to minimize issues which can occur from merging branches using different assets.   As git is also designed to be safe with a high degree of resilience against file corruption through the use of hashes, it is also important that these considerations be made right away at the start of a new unity project as merging and resolving histories with binary data also presents issues.  Additional considerations must also be made when combining to separate code bases into one repository such as was done for both the back and front ends of our project to fit the  requirements of the hackathon.  Once all these minor technical software related hurdles were overcome, the real challenge of building our technical solution started to manifest.

The challenge started to appear as we began look into the raw GPS data reported by our devices.  GPS coordinates were hidden to us at first by the fact that we could not set breakpoints to inspect the data from the device.  This is a shortcoming of the technologies and target systems.  Unity simply does not support breakpoints using the IL2CPP on 64 bit applications.  ARkit requires iOS 11 and Apple requires all iOS 11 applications to be 64bit thus Unity ARkit apps just have to be run on the device for testing manually.  Captive to the restrictions of our devices and technology we were forced to look at debug logs for coordinates.  As expected there was some error and variance with these coordinates but without a database of GPS to compare samples with we not sure how much of an issue this would be.  We knew the simple solution was to take the approach most similar applications just have proximity of the device to the target trigger a random encounter that would happen somewhere in the view of the camera but to better fit the vision of our design we chose to pursue a consistent universal experience.  It was important to have a note appear in the same physical place much in the same manner as a sticky note.  With this goal in mind we start to engineer the back end. 

As universal accessibility to information is a big part of the vision of out project, all devices need to be able to access the same data store to get and save notes.  Thus our application needed to be able to communicate with a remote database to deliver the same experience to all users.  Once we had a final design for user interaction it was just a matter of setting up an appropriate database schema and ensuring that out application could communicate with it correctly.  Since communicating the back end does not actually require use of any ARkit specific features, debugging communications issues was comparatively a simple issue compared to the main challenge which started to appear as the back end got populated with real coordinates.   

The real technical challenge of building a geospatially aware augmented reality application like this one is to merge the coordinates systems used by GPS with those of the camera and game world where all the virtual objects exist.  The main issue comes from try to resolve the sweet spot to compensate for all noise between the associated inputs.  On one sdie of the input there is the noise from the GPS coordinates of the device itself which is only accurate up to a certain degree of error is also highly dependent on compass calibration.  On the other side there is noise is Arkit tracking and from the sensors on the phone itself which tie into it.  In a perfect world where one can depend on the compass to give an accurate heading and thus GPS coordinates, the math to resolve the coordinates with respect to the camera and position them within Unity is actually fairly straight forward.  One can take the delta of the GPS coordinates of a saved object such as a note and the current GPS coordinates of the device which is the point in real world at which the camera is to derive a vector from one to the other.  Using the compass heading of the camera on can then perform some linear algebra to derive the angle between the normal of the camera and this  vector and thus distance offset from the camera which is at the origin the the game world.

In practice there are many factors which introduce error at many levels of device interaction when building a naive solution like this.  Electromagnetic fields which pervasive in modern life all introduce tiny amounts of error to the compass heading  and which cause fluctuations that can throw off gps information.  Even a small error in the compass heading data can compound errors when dealing with GPS coordinates.  Without an objective way to determine the true heading or which set of coordinates is authoritatively correct the best one can do is try to compensate for the error by having the user do basic calibration so that one can approximate the location of where a virtual object should be.   There are number of ways to do this which can work to provide a relatively smooth user experience were objects more reliably appear where they were left in the world but ultimately there is still some noise from the associated sensors which cannot be overcome.  There is also a slight issue that the radial coordinate system used by GPS systems assume that the earth us a perfect sphere which it not.  In spite of all of this is still possible to create a usable experience which at least demonstrates the core idea of our application.  

## Accomplishments

## Conclusion?

Virtual notes are saved to a globally accessible database which allows users to retrieve them anywhere.  Notes are equally accessible to everyone and are universally identifiable by their simple recognizable design.  Each note holds some information and left in a public space it is there to communicate value to another mind.  While notes may not always appear exactly where they were left due to the technical issues explained above but the data held with in each note remains and communicates what the messenger left behind wherever it appear in the world.  
