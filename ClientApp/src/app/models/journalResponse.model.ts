export interface JournalResponse {
    course: number,
    group: string,
    subjectName: string,
    studentPresenceInfos: StudentPresenceInfo[]
}

export interface StudentPresenceInfo {
    name: string,
    isPresent: boolean
}