import { Guid } from "guid-typescript"

export interface ProgramResultCreate {
    name: string
    description: string
    educationalProgramId: Guid
}
