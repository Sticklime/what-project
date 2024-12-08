pipeline {
    agent any

    environment {
        UNITY_PATH = "/home/unitybuild/Unity/Hub/Editor/6000.0.29f1/Editor/Unity"
        PROJECT_PATH = "/home/unitybuild/what-project"
        BUILD_PATH = "${PROJECT_PATH}/Builds/LinuxServer"
        EXECUTABLE_NAME = "LinuxServer" // Имя исполняемого файла
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

        stage('Abort Previous Builds') {
            steps {
                script {
                    def builds = currentBuild.rawBuild.getParent().getBuilds()
                    builds.each { build ->
                        if (build.isBuilding() && build.getNumber() != currentBuild.number) {
                            try {
                                // Остановка билда
                                build.doKill()
                                echo "Build #${build.getNumber()} aborted."
                            } catch (Exception e) {
                                echo "Failed to abort build #${build.getNumber()}: ${e.getMessage()}"
                            }
                        } else if (!build.isBuilding() && build.getNumber() != currentBuild.number) {
                            try {
                                // Удаление логов (пометка как неактивный)
                                build.keepLog(false)
                                echo "Build #${build.getNumber()} marked as inactive."
                            } catch (Exception e) {
                                echo "Failed to mark build #${build.getNumber()} as inactive: ${e.getMessage()}"
                            }
                        }
                    }
                }
            }
        }

        stage('Mark Old Builds as Inactive') {
            steps {
                script {
                    def builds = currentBuild.rawBuild.getParent().getBuilds()
                    builds.each { build ->
                        if (build.getNumber() != currentBuild.number) {
                            echo "Attempting to mark build #${build.getNumber()} as inactive."
                            try {
                                // Попытка отключить лог
                                build.keepLog(false)
                                echo "Build #${build.getNumber()} marked as inactive."
                            } catch (org.jenkinsci.plugins.scriptsecurity.sandbox.RejectedAccessException ex) {
                                // Если метод недоступен, просто сообщить об этом
                                echo "Permission denied to use keepLog(false) for build #${build.getNumber()}. Skipping."
                            }
                        }
                    }
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
                sh """
                    chmod +x "${BUILD_PATH}"
                    "${BUILD_PATH}" -batchmode -nographics
                """
            }
        }
    }
}