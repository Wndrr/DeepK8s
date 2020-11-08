![CI/CD - UnitTests](https://github.com/Wndrr/DeepK8s/workflows/CI/CD%20-%20Run%20test%20&%20Generate%20report/badge.svg)

![Analysis - SonarQube (Code Health)](https://github.com/Wndrr/DeepK8s/workflows/Analysis%20-%20SonarQube%20(Code%20Health)/badge.svg)

![Analysis - CodeQL (Security)](https://github.com/Wndrr/DeepK8s/workflows/Analysis%20-%20CodeQL%20(Security)/badge.svg)

# Deep K8s
This project is in the heavy WIP phase. It shouldn't break your cluster but, just in case, I'll state it here:

I SHAN'T BE HELD RESPONSIBLE IF THE USAGE OF THIS SOFTWARE CRASHES YOUR K8S CLUSTER OR OPENS A PORTAL TO HELL.

The pre-releases can be downloaded from [this page](https://github.com/Wndrr/DeepK8s/releases)

## What is the woot ?

Pronounce "Deep Case" (diːp keɪs) is an overview, dashboard and administration tool for Kubernetes. It is currently in the indev stage, missing most of the features and polishing.

This project aims at being an alternative to the kubectl command line aswell as an helper project for its usage. Many of the operations normally executed through the kubectl CLI can be performed within the Deep K8s GUI but the executed commands are also available to copy/paste if need be.

## Plateforms
At the moment, the executables have been compiled only for windows.
The software was tested on windows 10 64 bits exclusively.

## Documentation

Not much doc for now ... All you really need to know is that the software will look for your cluster configuration in the `/user/***/.kube` folder (as per the kubectl's default behaviour) 

## Motivation

The origin of this project is the absence of a similar software currently on the market with *all* of the following:

Feature-riche, beautiful UI, ergonomically optimized UI, fast response times, cross-platform and generally speaking an all-in-one solution for cluster management.

## Tech

The thinggy runs on [blazor](https://dotnet.microsoft.com/apps/aspnet/web-apps/blazor) server-side hosted in a [Electron.NET](https://github.com/ElectronNET/Electron.NET)
