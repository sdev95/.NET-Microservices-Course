#!/bin/bash

# RUN ON WINDOWS
# bash ./kill-deployments.sh

# RUN ON WSL / LINUX
# ./kill-deployments.sh

NORMAL='\033[0;37m'
GREEN='\033[0;32m'

echo -e "${GREEN}Running kill-deployments.sh"

kubectl delete all --all --all-namespaces

echo -e "Succesfully deleted all deployments ${NORMAL}"