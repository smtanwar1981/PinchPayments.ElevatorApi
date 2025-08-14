# Elevator Route Planner API

This project is a simple **.NET API** that plans elevator routes based on passenger onboard and offboard requests.

## Overview

The API receives:
- The starting floor of the elevator.
- A list of passengers with their source and destination floors.

It returns:
- An ordered route showing at each floor:
  - Passengers who **onboard**.
  - Passengers who **offboard**.

---

## API Endpoint

**POST** `/api/Elevator/getElevatorRoute`

### Request Parameters

| Parameter       | Type                      | Description                     |
|-----------------|---------------------------|---------------------------------|
| `initialLevel`  | `int`                     | The starting level of the elevator |
| `requests`      | `List<SummonRequest>`     | List of passenger onBoard/offBoard requests |

**SummonRequest**
```json
{
  "name": "string",
  "sourceLevel": "int",
  "destinationLevel": "int"
}