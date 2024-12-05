pipeline {
    agent any

    environment {
        UNITY_PATH = "/home/unitybuild/Unity/Hub/Editor/6000.0.29f1/Editor/Unity"
        PROJECT_PATH = "/home/unitybuild/what-project"
        BUILD_PATH = "${PROJECT_PATH}/Builds/LinuxServer"
        UNITY_USERNAME = "kol.dunin@yandex.ru"
        UNITY_PASSWORD = "Galaxys3"
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
            }
        }

        stage('Build Linux Server') {
            steps {
                sh '''
                ${UNITY_PATH} \
                    -batchmode \
                    -nographics \
                    -logFile - \
                    -username "${UNITY_USERNAME}" \
                    -password "${UNITY_PASSWORD}" \
                    -projectPath ${PROJECT_PATH} \
                    -executeMethod CodeBase.Build_CI.Editor.BuildScript.BuildLinuxServer \
                    -quit
                '''
            }
        }

        stage('Run Linux Server Build') {
            steps {
                sh '''
                chmod +x ${BUILD_PATH}
                ${BUILD_PATH}
                '''
            }
        }
    }

    post {
        always {
            echo "Pipeline completed."
        }
        success {
            echo "Build and deployment succeeded!"
        }
        failure {
            echo "Build or deployment failed."
        }
    }
}
