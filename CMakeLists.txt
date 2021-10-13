cmake_minimum_required(VERSION 3.16)

project(NadekoBot
    LANGUAGES NONE
    VERSION 3.0.7
)

include(GNUInstallDirs)

add_custom_target(
        build ALL
        COMMENT "dotnet publish"
        COMMAND dotnet publish -c $<CONFIG>
        WORKING_DIRECTORY ${CMAKE_SOURCE_DIR}
        VERBATIM
)

install(DIRECTORY ${CMAKE_SOURCE_DIR}/src/NadekoBot/bin/${CMAKE_BUILD_TYPE}/net5.0/linux-x64/publish/ DESTINATION ./nadekobot) #  FILES_MATCHING PATTERN ".pdb" EXCLUDE


#  CPack
# -----------------------
set(CPACK_PACKAGE_CONTACT "Edgar@AnotherFoxGuy.com")
set(CPACK_PACKAGE_VENDOR "AnotherFoxGuy")
set(CPACK_PACKAGING_INSTALL_PREFIX "/opt")
set(CPACK_GENERATOR STGZ;ZIP;DEB)
set(CPACK_PACKAGE_FILE_NAME "NadekoBot")
set(CPACK_DEBIAN_COMPRESSION_TYPE xz)

include(CPack)

# ./NadekoBot.sh --prefix=test --exclude-subdir --skip-license