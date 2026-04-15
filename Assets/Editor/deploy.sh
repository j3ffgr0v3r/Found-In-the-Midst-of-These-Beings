#!/bin/bash

# Build output folder
BUILD_DIR="Builds/WebGL"

# Temp folder for deployment
DEPLOY_DIR="temp-deploy"

# Remove old temp
rm -rf $DEPLOY_DIR

# Copy build
cp -r $BUILD_DIR $DEPLOY_DIR

cd $DEPLOY_DIR

# Init new git repo
git init
git checkout -b pages

# Add .nojekyll
touch .nojekyll

git add .
git commit -m "Deploy WebGL build"

# Push to GitHub
git remote add origin https://github.com/j3ffgr0v3r/Found-In-the-Midst-of-These-Beings.git
git push -f origin pages

cd ..
rm -rf $DEPLOY_DIR

echo "Deployment complete!"