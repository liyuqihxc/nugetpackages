#!/usr/bin/env bash
##########################################################################
# This is the Cake bootstrapper script for Linux and OS X.
# This file was downloaded from https://github.com/cake-build/resources
# Feel free to change this file to fit your needs.
##########################################################################

# Define directories.
SCRIPT_DIR=$( cd "$( dirname "${BASH_SOURCE[0]}" )" && pwd )
TOOLS_DIR=$SCRIPT_DIR/tools
CAKE_VERSION=0.32.1
CAKE_DLL=$TOOLS_DIR/Cake.CoreCLR.$CAKE_VERSION/Cake.dll

# Define default arguments.
SCRIPT="build.cake"
CAKE_ARGUMENTS=()

# Parse arguments.
for i in "$@"; do
  case $1 in
    -s|--script) SCRIPT="$2"; shift ;;
    --) shift; CAKE_ARGUMENTS+=("$@"); break ;;
    *) CAKE_ARGUMENTS+=("$1") ;;
  esac
  shift
done

# Make sure the tools folder exist.
if [ ! -d "$TOOLS_DIR" ]; then
  mkdir "$TOOLS_DIR"
fi

if ! dotnet_loc="$(type -p "dotnet")" || [[ -z $dotnet_loc ]]; then
  echo "This bootstrapper dependends on .Net Core CLI."
  exit 1
fi


###########################################################################
# INSTALL CAKE
###########################################################################

if [ ! -f "$CAKE_DLL" ]; then
  echo "Installing Cake..."
  curl -Lsfo Cake.CoreCLR.zip "https://www.nuget.org/api/v2/package/Cake.CoreCLR/$CAKE_VERSION" && unzip -q Cake.CoreCLR.zip -d "$TOOLS_DIR/Cake.CoreCLR.$CAKE_VERSION" && rm -f Cake.CoreCLR.zip
  if [ $? -ne 0 ]; then
    echo "An error occured while installing Cake."
    exit 1
  fi
fi

# Make sure that Cake has been installed.
if [ ! -f "$CAKE_DLL" ]; then
  echo "Could not find Cake.dll at '$CAKE_DLL'."
  exit 1
fi

###########################################################################
# RUN BUILD SCRIPT
###########################################################################

# Start Cake
echo "Running build script..."
exec dotnet "$CAKE_DLL" $SCRIPT "${CAKE_ARGUMENTS[@]}"
