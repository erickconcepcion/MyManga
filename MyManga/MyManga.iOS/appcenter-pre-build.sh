#!/usr/bin/env bash

if [ ! -n "$IOS_SECRET" ]
then
    echo "You need define the IOS_SECRET variable in App Center"
    exit
fi

APP_CONSTANT_FILE=$APPCENTER_SOURCE_DIRECTORY/MyManga/MyManga/DeployConstants.cs

if [ -e "$APP_CONSTANT_FILE" ]
then
    echo "Updating IOSSecret to $IOS_SECRET in DeployConstants.cs"
    sed -i '' 's#IOSSecret = "[a-z:./]*"#IOSSecret = "'$IOS_SECRET'"#' $APP_CONSTANT_FILE

    echo "File content:"
    cat $APP_CONSTANT_FILE
fi
