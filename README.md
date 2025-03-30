# Ultima-Adventures
A fork of Ultima Odyssey, A unique twist on Ultima Online - full server scripts, save, client.

Scripts (both core and non-core) for the Ultima Adventures project.  I've worked on this project and maintained the code (with the help of many contributors) for the last three years (2020-2023).  It's been challenging, fun and now time for me to move on to other projects. 

I can't claim all of the code here, as a large portion of it was taken by public scripts and coded by Ultima Odyssey dev Djeryv and taken from RunUO and Servuo script repositories, however this code does have some pretty unique and itneresting systems like the soulbound system, red/blue AI, completely redone champ spawn AI, a from-scratch magery AI, etc etc.  I leave my contribution to this codebase to the open source community with the hopes that some of it may be of use to propagate and keep the Ultima Online legacy alive for new generations to enjoy.

The original fork thread, discussion, feature list, etc can be found on servuo forums:  https://www.servuo.com/archive/ultima-adventures-a-full-featured-content-packed-offline-online-server.1374/

How to play/Use this git

The Git is meant to be used together with a client package you can download in the release area.  To play:

1. clone this git (or any other branch of this git you want) on your local hard drive
2. make sure your cloned git is in c:\Ultima-Adventures (else some editing is required in myserversettings.cs)
3. MyServerSettings.cs script needs to be edited depending on what system you are using.  Search for FilesPath and change depending on which system you are using.
4. Extract the Saves Archive into the main directory (so you have C:\Ultima-Adventures\Saves)
5. Download the drop-in additional files in the release tab, extract everything inside the Ultima-Adventures folder in the archive to your C:\Ultima-Adventures folder
6. Run the executable to run your own server, or run the runme options to launch the client

Alternatively, you can just download the entire package and not bother with cloning the git by downloading the Entire server package and extracing to your C:\ but you will be missing some of the latest changes applied to the code.

All the best to the UO community, I hope to keep playing this amazing game for many more years to come.  Feel free to clone and revise as you wish!

Note:  This code is completely open for you to use in any way you want.  Because the code depends on Runuo, Servuo, Djeryv, myself and hundreds of other contributors, we ask that you return the favor and make your server code/changes freely available.  The codebase benefits overall when everyone adds to it in their own personal capacity.

### MacOs Containerized Linux Development

Steps to get started:
1. Ensure you have `brew` installed
2. clone repo, make it your working directory
3. `make dev-deps` to install contents of Brewfile
4. Start the Orbstack agent, `⌘ + Space` to open spotlight and type Orbstack.
5. `make compile` to build the linux binary you'll need
6. `make build` to build the containerized image
7. `make run` to start the container and boot up the server!
8. Changes in code can be re-loaded into the container via `make rebuild`