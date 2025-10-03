# Executing the code

You can build and run the code in visual studio or by the .exe in the bin/debug / bin/release folder.

It will open up the browser and go to the swagger rest api. You can then hit the end points there in order to test the uesr stories.

The url for swagger that opens up should be https://localhost:7005/index.html

# Assumptions Made

- Populated some data with a simple sql lite database using C# entity framework code first. I know in a real world example there would be data already there, or technically we could have used a third party API to pull stock information, but felt that was to easy to do.

- There is currently one user (id: 1)

- Five transactions for that user. (id: 1-5)

# Coding Exercise
> This repository holds coding exercises for candidates going through the hiring process.

You should have been assigned one of the coding exercises in this list.  More details can be found in the specific md file for that assignment.

Instructions: Fork this repository, do the assigned work, and submit a pull request for review.

[Investment Performance Web API](InvestmentPerformanceWebAPI.md#investment-performance-web-api)

[Online Ordering SQL](OnlineOrderingSQL.md#online-ordering)

# License

```
Copyright 2021 Nuix

Licensed under the Apache License, Version 2.0 (the "License");
you may not use this file except in compliance with the License.
You may obtain a copy of the License at

   http://www.apache.org/licenses/LICENSE-2.0

Unless required by applicable law or agreed to in writing, software
distributed under the License is distributed on an "AS IS" BASIS,
WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
See the License for the specific language governing permissions and
limitations under the License.
```