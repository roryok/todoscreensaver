# todoscreensaver
A screensaver that reads a text file from somewhere on your PC. 

It doesn't *have* to be a todo list of course, that's just the best use case I can think of, and what I designed it for. 

## usage

1. `git clone https://github.com/roryok/todoscreensaver`
2. Compile using Visual Studio 2015
3. Rename the binary todoscreensaver.exe to todoscreensaver.scr
4. Right-click on todoscreensaver.scr and click "install"

## settings

So far there's only one setting - where to grab the text file from. I keep mine on dropbox so I can modify it remotely. 

## formatting the text file

No special formatting is required - we're literally just reading and displaying a text file. 

I like to start my todo item lines with utf8 symbols like the ones below 

- ☐ BALLOT BOX (U+2610) -- not yet completed task
- ✓ CHECK MARK (U+2713) -- completed task 
- ✗ BALLOT X (U+2717) -- cancelled or failed task 

## credits

The most complicated parts of this source are borrowed from Sean Sexton's Wave Sim Screensaver, available on CodePlex and licenced under GNU General Public License version 2 (GPLv2)

## licence

This app is licenced under GNU General Public License version 2 (GPLv2). I'm a noob when it comes to licencing, but I'm fairly sure I *have* to GPL it, because it uses some GPL'ed code. 
