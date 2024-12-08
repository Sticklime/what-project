pipeline {
    agent any

    environment {
        UNITY_PATH = "/home/unitybuild/Unity/Hub/Editor/6000.0.29f1/Editor/Unity"
        PROJECT_PATH = "/home/unitybuild/what-project"
        BUILD_PATH = "${PROJECT_PATH}/Builds/LinuxServer"
        EXECUTABLE_NAME = "LinuxServer" // Имя исполняемого файла
        SUDO_PASSWORD = "Galaxys3" // Пароль для sudo
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

        stage('Update Repository') {
            steps {
                script {
                    sh """
                        cd ${PROJECT_PATH}
                        sudo -S git reset --hard
                        sudo -S git pull
                        sudo -S chmod -R 775 ${PROJECT_PATH}
                        sudo -S chown -R jenkins:jenkins ${PROJECT_PATH}
                    """
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
                sh """
                    chmod +x ${BUILD_PATH}/${EXECUTABLE_NAME}
                    ${BUILD_PATH}/${EXECUTABLE_NAME}
                """
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
