import { Guid } from "guid-typescript"

export interface CompetenceCreate {
    name: string
    description: string
    educationalProgramId: Guid
}
