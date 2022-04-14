pipeline {
  agent {
    docker {
      image 'mcr.microsoft.com/dotnet/sdk:6.0'
    }

  }

  environment {
    DOTNET_CLI_HOME = "/tmp/DOTNET_CLI_HOME"
  }

  stages {
    stage('Restore packages') {
        steps {
            sh 'dotnet restore AV.AvA.sln'
        }
    }
    stage('Clean') {
        steps {
            sh 'dotnet clean AV.AvA.sln'
        }
    }
    stage('Build') {
        steps {
            sh 'dotnet --configuation Debug --no-restore build AV.AvA.sln'
        }
    }
    stage('Publish') {
        steps {
            sh 'dotnet --configuation Debug --no-restore publish AV.AvA.sln'
        }
    }
  }
}
