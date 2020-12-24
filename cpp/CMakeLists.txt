cmake_minimum_required(VERSION 3.10)

project(RestTest VERSION 1.0)

file(GLOB dependencies
    "src/*/*.cpp"
)

add_executable(RestTest
    main.cpp
    ${dependencies}
)


add_subdirectory(extern/googletest)
include_directories(extern/googletest/include)
add_executable(tests tests/tests.cpp ${dependencies})
target_link_libraries(tests PRIVATE gtest)

target_include_directories(tests PRIVATE ${CMAKE_CURRENT_SOURCE_DIR}/include)

# mkdir build
# cd build
# cmake ..
# make