cat ./ascii.txt

RED='\033[0;31m'
GREEN='\033[0;32m'
YELLOW='\033[0;33m'
BRED='\033[1;31m'
BGREEN='\033[1;32m'
ENDCOLOR='\033[0m'

export unityPath=/Applications/Unity/Hub/Editor/2019.4.28f1
echo '\n'
echo "Exporting package, please wait ...\n"
$unityPath'/Unity.app/Contents/MacOS/Unity' -batchmode -quit -executeMethod ScaleMonk.Ads.ExportScaleMonk.Export
# result=$(xmllint --xpath "string(//test-run/@result)" test-results.xml)
# testcasecount=$(xmllint --xpath "string(//test-run/@testcasecount)" test-results.xml)
# passed=$(xmllint --xpath "string(//test-run/@passed)" test-results.xml)
# failed=$(xmllint --xpath "string(//test-run/@failed)" test-results.xml)
# duration=$(xmllint --xpath "string(//test-run/@duration)" test-results.xml)
# if [ "$result" == "Passed" ]; then
#     coverage=$(xmllint --xpath "string(//CoverageReport/Summary/Linecoverage/text())" ./CodeCoverage/Report/Summary.xml)
# else 
#     coverage='N/A'
# fi

# printf '%s\n' "Tests Cases: $testcasecount" "Total Passed: $passed" "Total Failed: $failed" "Duration: $duration" "Coverage: $coverage"
# echo '\n'
# if [ "$result" == "Passed" ]; then
#     printf ${GREEN}Success${ENDCOLOR}
# else 
#     printf ${RED}Failure${ENDCOLOR}
# fi