@echo off
cmd /k newman run "collections/DrivingLicenseBackendAPITests.postman_collection.json" --environment "environments/MyEnvironment.postman_environment.json" --insecure
