cmake_minimum_required(VERSION 3.10)

project(RestTest VERSION 1.0)

file(GLOB dependencies
    main.cpp
    "src/*/*.cpp"
)

add_executable(RestTest
    ${dependencies}
)