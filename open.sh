#!/bin/sh

cd "$(dirname "$0")"
git pull
/Applications/Unity/Hub/Editor/2019.1.14f1/Unity.app/Contents/MacOS/Unity -projectPath . &
for f in *.sln; do open $f; done
