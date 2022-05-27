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
            sh 'dotnet build -c Release AV.AvA.sln --no-restore'
        }
    }
    stage('Test entityframework') {
        steps {
            sh 'dotnet tool install --global dotnet-ef'
            sh '/tmp/DOTNET_CLI_HOME/.dotnet/tools/dotnet-ef dbcontext info  --startup-project src/AV.AvA.StorageBackend/AV.AvA.StorageBackend.csproj --project src/AV.AvA.Data/AV.AvA.Data.csproj'
        }
    }
    stage('Publish') {
        agent {
            docker {
                image 'mcr.microsoft.com/dotnet/sdk:6.0'
            }
        }
        steps {
            sh 'dotnet workload install wasm-tools'
            sh 'dotnet publish "src/AV.AvA.StorageBackend/AV.AvA.StorageBackend.csproj" -c Release -o publish-backend --no-restore'
            sh 'dotnet publish "src/AV.AvA.BlazorWasmClient/AV.AvA.BlazorWasmClient.csproj" -c Release -o publish-frontend --no-restore'
            sh 'dotnet publish "src/AV.AvA.BackupTool/AV.AvA.BackupTool.csproj" -c Release -o publish-tool --no-restore /p:RunAOTCompilation=true'
            archiveArtifacts artifacts: 'publish-backend/, publish-frontend/, publish-tool/', followSymlinks: false, onlyIfSuccessful: true
        }
    }
  }
}
