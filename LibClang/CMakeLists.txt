# CMake 最低版本号要求
CMAKE_MINIMUM_REQUIRED(VERSION 2.6)

# 项目名称
PROJECT(PtsLib)

#head file path
INCLUDE_DIRECTORIES(
inc
)

# 添加 src 子目录
ADD_SUBDIRECTORY(src)

# 设置输出文件路径
SET(PROJECT_BINARY_DIR ./bin)
