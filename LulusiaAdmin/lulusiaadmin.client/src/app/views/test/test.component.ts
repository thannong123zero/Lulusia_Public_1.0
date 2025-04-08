import { Component } from '@angular/core';
import { ButtonDirective } from '@coreui/angular';
import { CkeditorComponent } from '@components/ckeditor/ckeditor.component';
import { ToastNotificationComponent } from '@components/toast-notification/toast-notification.component';
import { ToastService } from 'src/app/core/services/helper-services/toast.service';
import { CustomPaginationComponent } from "../../core/components/custom-pagination/custom-pagination.component";
import { DataTableComponent } from "../../core/components/data-table/data-table.component";

@Component({
  selector: 'app-test',
  imports: [CkeditorComponent, CustomPaginationComponent, DataTableComponent],
  templateUrl: './test.component.html',
  styleUrl: './test.component.scss'
})
export class TestComponent {
  content : string = "<div><p>Kính Gửi Nhà Thầu: <b>#ContractorName#</b></p><p>Email này được gửi đến từ hệ thống phê duyệt nhà thầu của Thiskyhall. Xin trân trọng thông báo bạn được tài khoản để theo dõi hoạt động sự kiện của bạn đã đăng ký tổ chức. <b>Sau khi đăng nhập bạn nên đổi mật khẩu để thuận tiện trong quá trình xử dụng phần mềm.</b></p><p>Thông tin tài khoản:</p><p>Username: <b>#UserName#</b></p><p>Password: <b>#Password#</b></p><p>Để đăng nhập vào hệ thống, vui lòng truy cập vào đường dẫn sau: <a href='#Link#'>Thiskyhall System</a></p><p>Thông tin sự kiện:</p><p>Tên sự kiện: <b>#EventName#</b></p><p>Thời gian: <b>#EventTime#</b></p><p>Trân trọng!</p></div>";
  initialDB: string = '';
  changeData(data:string){
    console.log(data);
  }
  onPageChange(page: number) {
    console.log('Current page:', page);
  }
  setData(){
    this.initialDB = this.content;
  }
}
