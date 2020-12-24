#include <gtest/gtest.h>
#include "configuration/Configuration.h"

TEST(ClasseTeste, MetodoTest)
{
    Configuration conf("a");
}

int main(int argc, char* argv[])
{
    ::testing::InitGoogleTest(&argc, argv);
    return RUN_ALL_TESTS();
}