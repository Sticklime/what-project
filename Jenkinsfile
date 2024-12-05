pipeline {
    agent any

    environment {
        UNITY_PATH = "/home/unitybuild/Unity/Hub/Editor/6000.0.29f1/Editor/Unity"
        PROJECT_PATH = "/home/unitybuild/what-project"
        BUILD_PATH = "${PROJECT_PATH}/Builds/LinuxServer"
        UNITY_USERNAME = credentials('unity-username')
        UNITY_PASSWORD = credentials('unity-password')
    }

    stages {
        stage('Kill Unity Processes') {
            steps {
                sh '''
                pkill -f Unity || true
                pkill -f UnityHub || true
                '''
            }
        }

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

        stage('Verify Build') {
            steps {
                sh '''
                if [ ! -f "${BUILD_PATH}/your_build_executable" ]; then
                    echo "Build failed: Executable not found!"
                    exit 1
                fi
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
            sh 'cat ${PROJECT_PATH}/Editor.log || echo "Log file not found."'
        }
    }
}
