pipeline {
  agent {
    docker {
      image 'mcr.microsoft.com/dotnet/sdk:6.0'
    }

  }
  stages {
    stage('build debug') {
      parallel {
        stage('build debug') {
          steps {
            sh 'dotnet publish AV.AvA.sln -c Debug'
            archiveArtifacts(artifacts: 'bin/', allowEmptyArchive: true, onlyIfSuccessful: true)
          }
        }

        stage('build release') {
          steps {
            sh 'dotnet publish AV.AvA.sln -c Release'
            archiveArtifacts(allowEmptyArchive: true, onlyIfSuccessful: true, artifacts: 'bin/')
          }
        }

      }
    }

  }
}