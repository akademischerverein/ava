pipeline {
  agent {
    docker {
      image 'mcr.microsoft.com/dotnet/sdk:6.0'
    }

  }
  stages {
    stage('Restore packages') {
        steps {
            sh 'dotnet restore AV.AvA.sln'
        }
    }
    stage('Clean') {
        steps {
            sh 'dotnet clean AV.AvA.sln --configuation Debug'
        }
    }
    stage('Build') {
        steps {
            sh 'dotnet build AV.AvA.sln --configuation Debug --no-restore'
        }
    }
    stage('Publish') {
        steps {
            sh 'dotnet publish AV.AvA.sln --configuation Debug --no-restore'
        }
    }
  }
}
