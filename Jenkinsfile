pipeline {
    agent any
    triggers {
        githubPush()
    }
    stages {
        stage('Restore packages'){
           steps{
               sh 'dotnet restore ConsoleApp_consumer.sln'
            }
         }
        stage('Clean'){
           steps{
               sh 'dotnet clean ConsoleApp_consumer.sln --configuration Release'
            }
         }
        stage('Build'){
           steps{
               sh 'dotnet build ConsoleApp_consumer.sln --configuration Release --no-restore'
            }
         }
        stage('Publish'){
             steps{
               sh 'dotnet publish ConsoleApp_consumer/ConsoleApp_consumer.csproj --configuration Release --no-restore'
             }
        }
        stage('Deploy'){
             steps{
               sh '''for pid in $(lsof -t -i:9090); do
                       kill -9 $pid
               done'''
               sh 'cd ConsoleApp_consumer/bin/Debug/net5.0/publish/'
               sh 'nohup dotnet ConsoleApp_consumer.dll --urls="http://104.128.91.189:9090" --ip="104.128.91.189" --port=9090 --no-restore > /dev/null 2>&1 &'
             }
        }
    }
}