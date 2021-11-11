#!/bin/bash

# RUN ON WINDOWS
# bash ./run-deployments.sh

# RUN ON WSL / LINUX
# ./run-deployments.sh

NORMAL='\033[0;37m'
GREEN='\033[0;32m'

# Running script
echo "Running run.sh"
echo "Starting deployments.."

# 
kubectl create secret docker-registry regcred --docker-server=https://index.docker.io/v1/ --docker-username=$DOCKER_USERNAME --docker-password=$DOCKER_PASSWORD --docker-email=$DOCKER_EMAIL
kubectl create secret generic mssql --from-literal=SA_PASSWORD="pa55w0rd!"

# Applying nginx controller
echo -e "${NORMAL}Apply ingress nginx controller."
kubectl apply -f https://raw.githubusercontent.com/kubernetes/ingress-nginx/controller-v1.0.4/deploy/static/provider/cloud/deploy.yaml
echo -e "${GREEN}Succesfully applied ingress nginx controller"

# Applying commands-depl.yaml
echo -e "${NORMAL}Apply commands-depl.yaml"
kubectl apply -f commands-depl.yaml
echo -e "${GREEN}Succesfully applied commands-depl.yaml"

# Applying localpvc-depl.yaml
echo -e "${NORMAL}Apply localpvc-depl.yaml"
kubectl apply -f local-pvc.yaml
echo -e "${GREEN}Succesfully applied localpvc-depl.yaml"

# Applying mssql-plat-depl.yaml
echo -e "${NORMAL}Apply mssql-plat-depl.yaml"
kubectl apply -f mssql-plat-depl.yaml
echo -e "${GREEN}Succesfully applied mssql-plat-depl.yaml"

# Applying platforms-depl.yaml
echo -e "${NORMAL}Apply platform-depl.yaml"
kubectl apply -f platforms-depl.yaml
echo -e "${GREEN}Succesfully applied platform-depl.yaml"

# Applying ingress-srv.yaml
echo -e "${NORMAL}Apply ingress-srv.yaml"
kubectl apply -f ingress-srv.yaml
echo -e "${GREEN}Succesfully applied ingress-srv.yaml"

# Applying rabbitmq-depl.yaml
echo -e "${NORMAL}Apply rabbitmq-depl.yaml"
kubectl apply -f rabbitmq-depl.yaml
echo -e "${GREEN}Succesfully applied rabbitmq-depl.yaml"

echo -e "Applied all deployments ${NORMAL}"