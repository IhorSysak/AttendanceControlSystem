export interface JournalRequest {
    course: number,
    group: string,
    date: Date,
    subject: SubjectModel
}

export interface SubjectModel {
    name: string,
    time: string
}