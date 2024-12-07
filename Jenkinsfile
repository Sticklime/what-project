pipeline {
    agent any

    environment {
        UNITY_PATH = "/home/unitybuild/Unity/Hub/Editor/6000.0.29f1/Editor/Unity"
        PROJECT_PATH = "/home/unitybuild/what-project"
        BUILD_PATH = "${PROJECT_PATH}/Builds/LinuxServer"
    }

    stages {
        stage('Abort Previous Builds') {
            steps {
                script {
                    // Jenkins script to abort running builds for the same job
                    def builds = currentBuild.rawBuild.getParent().getBuilds()
                    for (def b : builds) {
                        if (b.isBuilding() && b.getNumber() != currentBuild.number) {
                            b.doKill()
                            echo "Aborted build #${b.getNumber()}"
                        }
                    }
                }
            }
        }

        stage('Checkout Repository') {
            steps {
                checkout scm
                sh '''
                sudo chown -R jenkins:jenkins ${PROJECT_PATH}
                '''
            }
        }

        stage('Build Linux Server') {
            steps {
                script {
                    def status = sh(script: '''
                        ${UNITY_PATH} \
                        -batchmode \
                        -nographics \
                        -projectPath ${PROJECT_PATH} \
                        -executeMethod CodeBase.Build_CI.Editor.BuildScript.BuildLinuxServer \
                        -quit
                        ''', returnStatus: true)
                    if (status != 0) {
                        echo "Unity build failed. Check Editor.log for details."
                        sh 'cat ${PROJECT_PATH}/Editor.log'
                        error("Unity build failed with exit code ${status}")
                    }
                }
            }
        }

        stage('Fix Build Permissions') {
            steps {
                sh '''
                sudo chown -R jenkins:jenkins ${BUILD_PATH}
                sudo chmod +x ${BUILD_PATH}
                '''
            }
        }

        stage('Run Linux Server Build') {
            steps {
                sh '''
                # Run the build without graphics (headless mode)
                ${BUILD_PATH} -batchmode -nographics -logFile ${PROJECT_PATH}/server_log.txt
                '''
            }
        }
    }

    post {
        always {
            echo "Pipeline completed."
            sh 'cat ${PROJECT_PATH}/Editor.log || echo "Log file not found."'
        }
        success {
            echo "Build and deployment succeeded!"
        }
        failure {
            echo "Build or deployment failed."
            sh 'cat ${PROJECT_PATH}/Editor.log || echo "Log file not found."'
        }
    }
}
