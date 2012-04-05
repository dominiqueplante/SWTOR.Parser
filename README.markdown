SWTOR Combat Log Parser
=======================
Parses and store log files for the game "Star Wars, The Old Republic" http://www.swtor.com/.

See http://timrayburn.net/blog/introducing-swtor-dot-parser/ for original announcement of this project.

Deployed at http://swtor.apphb.com.

Overview
========
   
This project has three portions:

* Combat Parser website at http://swtor.apphb.com/
* JSON Parser available at http://swtor.apphb.com/API/ParseText.  Send the log text with the name "combatLog" and the response will be a parsed JSON object.
* .NET Library component for parsing combat logs, used in the above.  Grab the source code here, compile and reference.

For the Geeks
=============

Cool Stuff and Concepts
-----------------------
- ASP.NET MVC
- stores CombatLog Documents in Embedded RavenDB

Development
-----------
- NuGet for package management
- NCrunch
- Code Coverage can be calculating using R# 6.1 and DotCover 3.1
- unit tests for ASP.NET MVC

Tech Used for Development
-------------------------
- Visual Studio 2010
- ReSharper 6.1
- Resharper DotCover 1.1

Background Reading
------------------
- MSDN article on using RavenDB in an ASP.NET MVC web application

License
=======
<a rel="license" href="http://creativecommons.org/licenses/by/3.0/"><img alt="Creative Commons License" style="border-width:0" src="http://i.creativecommons.org/l/by/3.0/88x31.png" /></a><br /><span xmlns:dct="http://purl.org/dc/terms/" property="dct:title">SWTOR.Parser</span> by <a xmlns:cc="http://creativecommons.org/ns#" href="http://TimRayburn.net" property="cc:attributionName" rel="cc:attributionURL">Tim Rayburn</a> is licensed under a <a rel="license" href="http://creativecommons.org/licenses/by/3.0/">Creative Commons Attribution 3.0 Unported License</a>.<br />Based on a work at <a xmlns:dct="http://purl.org/dc/terms/" href="https://github.com/trayburn/SWTOR.Parser" rel="dct:source">github.com</a>.<br />Permissions beyond the scope of this license may be available at <a xmlns:cc="http://creativecommons.org/ns#" href="http://TimRayburn.net" rel="cc:morePermissions">http://TimRayburn.net</a>.