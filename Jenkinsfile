pipeline {
    agent any

    environment {
        DOCKER_REGISTRY = " "   // or leave empty for local
        GIT_REPO = "https://github.com/rjAhsan/testapp3.git"
    }

    stages {

        stage('Set Branch Config') {
            steps {
                script {
                    // Map branch → image name & container name & port
                    switch(env.BRANCH_NAME) {
                        case 'main':
                            env.IMAGE_NAME    = "webapp1"
                            env.CONTAINER_NAME = "webapp1"
                            env.HOST_PORT     = "8001"
                            break
                        case 'staging':
                            env.IMAGE_NAME    = "webapp2"
                            env.CONTAINER_NAME = "webapp2"
                            env.HOST_PORT     = "8002"
                            break
                        case 'dev':
                            env.IMAGE_NAME    = "webapp3"
                            env.CONTAINER_NAME = "webapp3"
                            env.HOST_PORT     = "8003"
                            break
                        default:
                            error("Unknown branch: ${env.BRANCH_NAME}")
                    }
                    echo "Branch: ${env.BRANCH_NAME} → Image: ${env.IMAGE_NAME} on port ${env.HOST_PORT}"
                }
            }
        }

        stage('Checkout') {
            steps {
                checkout scm
            }
        }

        stage('Build Docker Image') {
            steps {
                script {
                    def buildTag = "${env.IMAGE_NAME}:${env.BUILD_NUMBER}"
                    def latestTag = "${env.IMAGE_NAME}:latest"

                    sh """
                        docker build -t ${buildTag} -t ${latestTag} .
                    """
                    env.BUILD_TAG = buildTag
                }
            }
        }

        stage('Stop & Remove Old Container') {
            steps {
                script {
                    sh """
                        # Stop container if running
                        docker stop ${env.CONTAINER_NAME} || true

                        # Remove container if exists
                        docker rm ${env.CONTAINER_NAME} || true
                    """
                }
            }
        }

        stage('Run New Container') {
            steps {
                script {
                    sh """
                        docker run -d \
                            --name ${env.CONTAINER_NAME} \
                            --restart=always \
                            -p ${env.HOST_PORT}:80 \
                            ${env.BUILD_TAG}
                    """
                }
            }
        }

        stage('Clean Old Images') {
            steps {
                script {
                    // Remove dangling images to save disk space
                    sh "docker image prune -f"
                }
            }
        }

        stage('Health Check') {
            steps {
                script {
                    sleep(10)  // wait for container to start
                    sh """
                        docker ps | grep ${env.CONTAINER_NAME} \
                        && echo '✅ Container is running' \
                        || (echo '❌ Container failed to start' && exit 1)
                    """
                }
            }
        }
    }

    post {
        success {
            echo "✅ Deployed ${env.IMAGE_NAME} on port ${env.HOST_PORT}"
        }
        failure {
            echo "❌ Pipeline failed for branch ${env.BRANCH_NAME}"
            // Optional: add email/Slack notification here
        }
    }
}