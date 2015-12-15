# IHFF
International Haarlem Film Festival

----

##Git problemen oplossen##
Cannot merge because there are uncommitted changes. Commit or undo your changes before merging again. See the Output window for details.

    open git shell
    git status (hier zie je welke files unmodified zijn)
    git rm --cached "(fullpath to unmodified file)"
    git rm image
    ![alt tag](http://i.imgur.com/NvsjbOV.png)
    in visual studio commit de doorsteepte file
    ![alt tag](http://i.imgur.com/HLPJu6V.png)
    git rm image
    sync
    resolve conflicts - keep local
    commit merge
    sync

An error occurred. Detailed message: An error was raised by libgit2. Category = FetchHead (MergeConflict). 1 conflict prevents checkout

    open git shell
    git pull

