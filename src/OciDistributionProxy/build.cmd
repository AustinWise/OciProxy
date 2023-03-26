@echo off
setlocal

pushd %~dp0

dotnet publish --os linux --arch x64 -c Release -p:PublishProfile=gcp
IF NOT %ERRORLEVEL%==0 GOTO :fail

gcloud run deploy oci-proxy --allow-unauthenticated --region us-central1 --image us-central1-docker.pkg.dev/oci-proxy/oci-proxy-images/oci-proxy:1.0.0
IF NOT %ERRORLEVEL%==0 GOTO :fail

exit /b 0

:fail
echo failed
exit /b 1
