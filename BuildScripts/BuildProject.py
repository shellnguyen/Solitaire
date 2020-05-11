import os
import sys
import subprocess

def Build():
	platform = sys.argv[1]
	config = sys.argv[2]
	unityVersion = sys.argv[3]
	
	unityPath = os.environ[unityVersion]
	
	print(unityPath)
	
	pathToExe = os.path.join(unityPath, "Unity.exe")
	
	print(pathToExe)
	
	output = subprocess.check_output([pathToExe, "-quit", "-batchmode", "-logFile", "-executeMethod", "Builder." + platform + config])
	
	print(output)
		
	
Build()