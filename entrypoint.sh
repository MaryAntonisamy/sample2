#!/bin/bash

# Start the existing app in the background
dotnet ExistingApp.dll &

# Start your new app
dotnet NewApp.dll

# You might need to use more sophisticated process management based on your requirements
