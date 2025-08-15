import { Component,OnInit } from '@angular/core';
import { StudentsService } from '../students.service';
import { ActivatedRoute, Router } from '@angular/router';
import { Student } from '../../Models/ui-models/student.models';
import { GenderService } from '../../students/services/gender.service';
import { Gender } from '../../Models/ui-models/gender.models';
import { MatSnackBar } from '@angular/material/snack-bar';




@Component({
  selector: 'app-view-student',
  standalone: false,
  templateUrl: './view-student.component.html',
  styleUrl: './view-student.component.css'
})
export class ViewStudentComponent implements OnInit {
  studentId: string | null | undefined ;
  student:Student ={
id:"",
firstName:"",
lastName:"",
dateOfBirth:"",
email:"",
mobile:0,
genderId:"",
profileImageUrl:"",
gender:{
id:"",
description:"",
},
address:{
  id:"",
  physicalAddress:"",
  postalAddress:"",

  }
}
genderList: Gender[] = [];
isNewStudent: boolean = false;
header="";
displayProfileImageUrl="";



  /**
   *
   */
  constructor(private readonly studentsService:StudentsService, 
    private readonly route:ActivatedRoute,
    private readonly genderService:GenderService,
    private router : Router,
    private snackbar: MatSnackBar
    
  ) { }
  
  ngOnInit(): void {
  this.route.paramMap.subscribe((params) => {
    ;
    this.studentId = params.get("id");

    if (this.studentId === "add") {
      this.isNewStudent = true;
      this.header = "Yeni Öğrenci Ekle";
      this.SetImage();
    } else {
      this.isNewStudent = false;
      this.header = "Öğrenciyi Düzenle";
      

      // ✅ Sadece buraya taşı!
      this.studentsService.getStudent(this.studentId).subscribe(
        (success) => {
          this.student = success;
          this.SetImage();
        },
        (error) => {
          this.SetImage();
        }
      );
    }

    // ✅ Gender listesi her durumda yüklensin
    this.genderService.getGenderList().subscribe(
      (success) => {
        this.genderList = success;
      },
      (error) => {
        console.error("Error fetching gender data", error);
      }
    );
  });
}

  onUpdate(){
    this.studentsService.UpdateStudent(this.studentId!,this.student).subscribe(
      (success) => {
        this.router.navigateByUrl('/students');
        this.snackbar.open("Öğrenci Başarılı Bir Şekilde Güncellendi",undefined,{
          duration: 2000
        })
      },
      (error) => {
        this.snackbar.open("Öğrenci Güncellenemedi",undefined,{
          duration: 2000
        })
      }
    )
  }
  onDelete(){
    this.studentsService.DeleteStudent(this.student.id).subscribe(
      (success) => {
        this.router.navigateByUrl('/students');
        this.snackbar.open("Öğrenci Başarılı Bir Şekilde Silindi",undefined,{
          duration: 2000
        })
        setTimeout(() => {
          this.router.navigateByUrl('/students');
        },2000);
      },
      (error) => {
        this.snackbar.open("Öğrenci Silinemedi",undefined,{
          duration: 2000
        })
      }
    )
  }
  onAdd(){
   this.studentsService.addStudent(this.student).subscribe(
      (success) => {
        
        this.snackbar.open("Öğrenci Başarılı Bir Şekilde Eklendi",undefined,{
          duration: 2000
        })
        setTimeout(() => {
          this.router.navigateByUrl(`/students/${success.id}`);
        },2000);
      },
      (error) => {
        this.snackbar.open("Öğrenci Eklenemedi!",undefined,{
          duration: 2000
        });
      }
    )
  }
  SetImage()
  {
if(this.student.profileImageUrl)
{
this.displayProfileImageUrl = this.studentsService.getImagePath(this.student.profileImageUrl);
}
else{
  this.displayProfileImageUrl = "/assets/user.png";
}
}
uploadImage(event:any){
if(this.studentId)
{
const file:File = event.target.files[0];
this.studentsService.uploadImage(this.studentId,file).subscribe(
  (success) => {
    this.student.profileImageUrl = success;
    this.SetImage();
    this.snackbar.open("Resim Başarıyla Yüklendi",undefined,{
      duration: 2000
    })
    this.SetImage();
  },
  (error) => {
    this.snackbar.open("Resim Yüklenemedi",undefined,{
      duration: 2000
    })
  }
)
}


}
}