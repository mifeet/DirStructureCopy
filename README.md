DirStructureCopy - backup the structure of your files
================

DirStructureCopy is a tool for efficient capturing of the complete file structure of directories to an archive, without storing actual data.

Do you have a large film/music/whatever collection which is too large for a full backup, yet you want a way how to restore it in case of data loss? You can use DirStructureCopy! DirStructureCopy is a simple tool that lets you backup the complete file structure of your collection without storing the actual data. In case of data loss, you cannot restore data from DirStructureCopy backup but you will know exactly what you lost and need to re-download/restore.

Or you have a directory with many files and subdirectories which you need to search often? If you search by filenames and/or file attributes, pack the directory using DirStructureCopy. Searching in the resulting archive will be much faster than on the actual file system. 

*Tip: If you backup a large directory or the entire filesystem, compress the destination archive again to get a very space-efficient representation.*

Download
--------
TO BE DONE. For now, you can build the solution yourself using Visual Studio.

How it works
------------
DirStructureCopy is very simple - choose the source directory, destination archive and click `Copy directory structure`. The created destination archive will containe zipped contents of the source directory except that all files in the archive will have zero size. Thus only the structure of files and directoreis is preserved, *not the actual data*. 

Archive contents will also preserve original file size in the comment of each file and preserve original file attributes (such as modification time). In case some files cannot be accessed, it is reported in `dirstructurecopy.log` log file.


The archive can be customized with these options:

* *Remove subdirectory structure* - if checked, DirStructureCopy will flatten the directory structure and all files will be placed directly in the root of the destination archive.
* *Include .zip archives* - if checked, DirStructureCopy will also dive into contained zip archives (one level only) and copy their structure too. The destination archive will contain files named <code>*&lt;path to zip archive&gt;$&lt;path within the archive&gt;*</code>.


License
-------

DirStructureCopy is published as Open Source under [Apache License Version 2.0](http://www.apache.org/licenses/LICENSE-2.0.html). You are free to use it for both personal or commercial purposes.
