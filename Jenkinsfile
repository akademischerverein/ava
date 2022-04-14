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
            sh 'dotnet build -c Release AV.AvA.sln'
        }
    }
    stage('Test entityframework') {
        steps {
            sh 'dotnet tool install dotnet-ef'
            sh 'dotnet ef dbcontext info  --startup-project src/AV.AvA.StorageBackend/AV.AvA.StorageBackend.csproj --project src/AV.AvA.Data/AV.AvA.Data.csproj'
        }
    }
    stage('Publish') {
        steps {
            sh 'dotnet publish -c Release AV.AvA.sln'
            archiveArtifacts artifacts: 'bin/', followSymlinks: false, onlyIfSuccessful: true
        }
    }
  }
}
