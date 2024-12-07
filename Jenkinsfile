pipeline {
    agent any

    environment {
        UNITY_PATH = "/home/unitybuild/Unity/Hub/Editor/6000.0.29f1/Editor/Unity"
        PROJECT_PATH = "/home/unitybuild/what-project"
        BUILD_PATH = "${PROJECT_PATH}/Builds/LinuxServer"
        EXECUTABLE_NAME = "LinuxServer.x86_64" // Имя исполняемого файла
    }

    stages {
        stage('Abort Previous Builds') {
            steps {
                script {
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

        stage('Git Pull (Manual Approval)') {
            steps {
                script {
                    withCredentials([string(credentialsId: 'Galaxys3', variable: 'Galaxys3')]) {
                        echo "Performing git pull..."
                        sh """
                        echo "${SUDO_PASSWORD}" | sudo -S -k bash -c '
                        cd ${PROJECT_PATH}
                        git reset --hard HEAD  # Принудительный сброс изменений (если нужно)
                        git pull origin main  # Здесь используйте вашу ветку вместо "main", если необходимо
                        sudo chown -R jenkins:jenkins ${PROJECT_PATH}
                        sudo chmod -R 775 ${PROJECT_PATH}
                        '
                        """
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
                script {
                    def status = sh(script: """
                        ${UNITY_PATH} \
                        -batchmode \
                        -nographics \
                        -projectPath ${PROJECT_PATH} \
                        -executeMethod CodeBase.Build_CI.Editor.BuildScript.BuildLinuxServer \
                        -quit
                        """, returnStatus: true)
                    if (status != 0) {
                        echo "Unity build failed. Check Editor.log for details."
                        sh "cat ${PROJECT_PATH}/Editor.log"
                        error("Unity build failed with exit code ${status}")
                    }
                }
            }
        }

        stage('Run Linux Server Build') {
            steps {
                script {
                    def executablePath = "${BUILD_PATH}/${EXECUTABLE_NAME}"
                    sh """
                    chmod +x ${executablePath}
                    ${executablePath} -batchmode -nographics -logFile ${PROJECT_PATH}/server_log.txt
                    """
                }
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
