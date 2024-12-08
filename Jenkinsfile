pipeline {
    agent any

    environment {
        UNITY_PATH = "/home/unitybuild/Unity/Hub/Editor/6000.0.29f1/Editor/Unity"
        PROJECT_PATH = "/home/unitybuild/what-project"
        BUILD_PATH = "${PROJECT_PATH}/Builds/LinuxServer"
        EXECUTABLE_NAME = "LinuxServer" // Имя исполняемого файла
        LOG_FILE = "/var/log/linux_server.log" // Путь к лог-файлу
    }

    stages {
        stage('Update Repository') {
            steps {
                script {
                    sh """
                        cd "${PROJECT_PATH}"
                        sudo git reset --hard
                        sudo git pull
                        sudo chmod -R 775 "${PROJECT_PATH}"
                        sudo chown -R jenkins:jenkins "${PROJECT_PATH}"
                    """
                }
            }
        }

        stage('Build Linux Server') {
            steps {
                script {
                    def status = sh(script: """
                        "${UNITY_PATH}" \
                        -batchmode \
                        -nographics \
                        -projectPath "${PROJECT_PATH}" \
                        -executeMethod CodeBase.Build_CI.Editor.BuildScript.BuildLinuxServer \
                        -quit
                    """, returnStatus: true)
                    if (status != 0) {
                        echo "Unity build failed. Check Editor.log for details."
                        sh "cat \"${PROJECT_PATH}/Editor.log\""
                        error("Unity build failed with exit code ${status}")
                    }
                }
            }
        }

        stage('Run Linux Server Build') {
            steps {
                script {
                    sh """
                        nohup "${BUILD_PATH}" -batchmode -nographics > "${LOG_FILE}" 2>&1 &
                    """
                    echo "Linux Server started. Logs are being written to ${LOG_FILE}"
                }
            }
        }
    }

    post {
        always {
            echo "Pipeline finished. Server started in background."
        }
    }
}
