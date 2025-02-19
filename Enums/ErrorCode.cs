using System.ComponentModel;

namespace BaseApi.Enums
{
    public enum ErrorCode
    {
        [Description("Failed")] FAILED = -1,
        [Description("Success")] SUCCESS = 0,
        [Description("Token Invalid")] TOKEN_INVALID = 2,
        [Description("System error")] SYSTEM_ERROR = 3,
        [Description("Database failed")] DB_FAILED = 4,

        [Description("Thư mục chứa ảnh chưa được cấu hình")]
        FOLDER_IMAGE_NOT_FOUND = 5,

        [Description("Định dạng tập tin không được hỗ trợ")]
        DOES_NOT_SUPPORT_FILE_FORMAT = 6,
        [Description("Not found")] NOT_FOUND = 7,
        [Description("Định dạng sai!")] INVALID_PARAM = 8,
        [Description("Exists")] EXISTS = 9,
        [Description("Key cert invalid")] INVALID_CERT = 10,
        [Description("Bad request")] BAD_REQUEST = 400,
        [Description("Unauthorization")] UNAUTHOR = 401,

        [Description("User locked")] USER_LOCKED = 20,

        [Description("Tài khoản đã bị khóa. Vui lòng liên hệ Admin để biết thêm chi tiết")]
        ACCOUNT_LOCKED = 23,

        [Description("Mật khẩu không chính xác. Vui lòng thử lại")]
        INVALID_PASS = 24,

        [Description("Tài khoản không tồn tại. Vui lòng kiểm tra lại")]
        ACCOUNT_NOTFOUND = 25,

        [Description("Nhân viên không tồn tại. Vui lòng kiểm tra lại")]
        USER_NOTFOUND = 26,

        [Description("Nhóm không tồn tại. Vui lòng kiểm tra lại")]
        TEAM_NOTFOUND = 27,

        [Description("Thêm nhân viên thất bại")]
        INSERT_FAIL = 28,

        [Description("Code đã tồn tại")] CODE_ALREADY_TAKEN = 29,

        [Description("Đã nhập thiếu trường bắt buộc trong excel")]
        EMPTY_REQUIRE_EXCEL = 30,

        [Description("Mật khẩu cũ  không chính xác. Vui lòng thử lại")]
        OLD_PASS_WRONG = 31,

        [Description("Sai định dạng ngày")] WRONG_FORMAT_DATE = 32,

        [Description("Trong file đã có mã code này")]
        DUPLICATE_CODE = 33,
        [Description("Mã OTP không hợp lệ")] INVALID_OTP = 34,
        [Description("Mã OTP đã hết hạn")] EXPIRED_OTP = 35,

        [Description("Trong file đã có mã email này")]
        DUPLICATE_EMAIL = 36,

        [Description("Email hoặc Mật khẩu sai ! Vui lòng thử lại")]
        WRONG_LOGIN = 37,

        [Description("Tài khoản không có quyền truy cập!")]
        FORBIDDEN_LOGIN = 38,


        [Description("Nhân viên này đã có tài khoản!")]
        USER_ALREADY_HAVE_ACC = 39,


        [Description("Địa điểm này không tồn tại!")]
        LOCATION_NOT_EXISTS = 40,

        [Description("Không tìm thấy chi nhánh!")]
        BRANCHES_NOTFOUND = 41,

        [Description("Không tìm thấy nhà thầu!")]
        CONTRACTOR_NOTFOUND = 42,

        [Description("Không tìm thấy quy trình!")]
        TASK_NOTFOUND = 43,

        [Description("Missing required parameter")]
        PARAMETER_IS_MISSING = 44,

        [Description("Activity has no child")] ACTIVITY_HAS_NO_CHILD = 45,

        [Description("Hành động không được phép")]
        ACTION_NOT_PERMITTED = 46,

        [Description("Không tìm thấy dự án!")] PROJECT_NOTFOUND = 47,

        [Description("Thiếu thông tin UUID")] MISSING_UUID = 48,


        [Description("Tài khoản không thể xóa!")]
        CANT_DELETE_ACC = 50,


        [Description("Không tìm thấy thông tin kế hoạch vốn hàng năm của dự án!")]
        PROJECT_ANNUAL_NOTFOUND = 51,


        [Description("Không tìm thấy nhóm nhà cung cấp!")]
        CONTRACTOR_CAT_NOT_FOUND = 52,

        [Description("Kiểu vốn không hợp lệ!")]
        INVALID_FUND_TYPE = 53,

        [Description("Số tiền vượt quá ngân sách!")]
        AMOUNT_OVER_BUDGET = 54,

        [Description("Năm không hợp lệ!")] INVALID_YEAR = 55,

        [Description("Kế hoạch năm đã tồn tại!")]
        PROJECT_ANNUAL_EXISTED = 56,

        [Description("Không được xóa nhân viên này !")]
        CANT_DELETE_USER = 60,

        [Description("Nhà thầu này đã có trong dự án!")]
        CONTRACTOR_ALREADY_IN_PROJECT = 61,

        [Description("Nhóm nhà thầu của dự án đã có nhà thầu hoạt động!")]
        PROJECT_CONTRACTOR_GROUP_ALREADY_HAS_ACTIVE_CONTRACTOR = 62,

        [Description("Truy cập bị từ chối!")] ACCESS_DENIED = 63,
        

            [Description("Tên đăng nhập đã được sử dụng! Vui lòng nhập tên khác!")]
        USERNAME_ALREADY_TAKEN = 64,

        [Description("Không được xóa nhóm nhà thầu này !")]
        CANT_DELETE_CONTRACTOR_CAT = 65,
        [Description("Không được xóa nhà thầu này !")]
        CANT_DELETE_CONTRACTOR = 66,
        [Description("Không được xóa chi nhánh này !")]
        CANT_DELETE_BRANCHES = 67 ,


            [Description("Tài khoản không tồn tại. Vui lòng kiểm tra lại")]
        ACCOUNT_DELETED = 70,
        
        [Description("Không đủ quyền thực hiện hành động")]
        ACTION_FORBIDDEN = 71,

        [Description("Chỉ được xóa dự án khi dự án đang chuẩn bị!")]
        CANT_DELETE_PROJECT = 72,

        [Description("Tổng dự toán không được lớn hơn Tổng mức đầu tư dự án!")]
        REAL_BUDGET_HIGHER = 73,

        [Description("Vốn dự phòng không được lớn hơn Tổng dự toán")]
        RESERVE_BUDGET_HIGHER = 74,

        [Description("Tên chi nhánh không được để trống")]
        BRANCH_NOT_NULL = 75,

        [Description("Tên công trình không được để trống")]
        NAME_NOT_NULL = 76,

        [Description("Thời gian kết thúc dự kiến không được nhỏ hơn thời gian bắt đầu dự kiến!")]
        EXPECT_END_SMALLER = 77,

        [Description("Tổng mức đầu tư dự án không được lớn hơn Kế hoạch vốn đầu tư")]
        TOTAL_INVEST_HIGHER = 78,

        [Description("Không tìm thấy báo cáo giải ngân!")] 
        PROJECT_FUND_NOTFOUND = 79,



        [Description("Email hoặc Số điện thoại của người dùng đã được sử dụng! Vui lòng dùng thông tin khác")]
        CANT_CREATE_USER = 80,

        [Description("Mã số thuế của nhà thầu đã có trong hệ thống!")]
        CONTRACTOR_WITH_CODE_ALREADY_HAVE = 81,

        [Description("Không tìm thấy báo cáo tổng quát!")]
        OVERVIEW_REPORT_NOTFOUND = 82,

        [Description("Đã có báo cáo giải ngân cho tháng này")]
        EXIST_FUND = 83,

        [Description("Không tìm thấy báo cáo giải ngân hoặc báo cáo công việc của nhân viên trong thời gian này!")]
        NOTFOUND_FUND_AND_REPORT = 84,

        [Description("Bạn cần tạo báo cáo công việc và báo cáo này cần được PM duyệt!")]
        UNCENSORED_REPORT = 85,

        [Description("Bạn cần tạo báo cáo giải ngân và báo cáo này cần được PM duyệt!")]
        UNCENSORED_FUND = 86,

        [Description("Bạn cần tạo kế hoạch tháng tiếp theo!")]
        NOT_FOUND_NEXT_MONTH_REPORT = 87,

        [Description("Báo cáo tổng hợp tháng này đã được gửi!")]
        AlREADY_HAVE_OVERVIEW_REPORT_IN_THIS_TIME = 88,
        [Description("Không thể xóa Nhóm quy trình này!")]
        CANT_DELETE_TASK_CAT = 89,

        [Description("Không thể kết thúc dự án do có công việc của dự án chưa kết thúc !")]
        CANT_END_PROJECT = 90,

        [Description("Mã OTP đã được sử dụng!")] OTP_USED = 91,

        [Description("Quy trình với tên này đã có. Vui lòng đổi tên file rồi thử lại !")] TASK_CAT_WITH_THIS_NAME_ALREADY_HAVE = 92,
        [Description("Không tìm thấy thông tin activity")]
        ACTIVITY_NOTFOUND, 



            [Description("Không tìm thấy quy trình này !")]
        TASK_CAT_NOTFOUND,
        
        [Description("Công việc chưa được đăng ký thực hiện")]
        ACTIVITY_NOT_REGISTERED,
        [Description("Activity đã kết thúc, không thể thực hiện báo cáo!")]
        ACTIVITY_CLOSED,
        
        [Description("Hành động không hợp lệ")]
        ACTION_INVALID,
        
        [Description("Chỉ được đăng ký vào ngày 1-5 hàng tháng")]
        REGISTER_DATE_EXPIRED,
        
        [Description("Tháng đăng ký không hợp lệ")]
        REGISTER_MONTH_INVALID,
        
        [Description("Có công việc đã được đăng ký thực hiện trước đó")]
        ACTIVITY_REGISTERED,
        
        [Description("Đã tạo báo cáo đăng ký hoạt động cho tháng này")]
        REPORT_ALREADY_EXIST,
        
        [Description("Nhân viên không thuộc dự án")]
        USER_NOT_IN_PROJECT,
        
        [Description("Không tìm thấy báo cáo")]
        REPORT_NOT_FOUND,
        
        [Description("Không tìm thấy thông tin nhà thầu")]
        PROJECT_CONTRACTOR_NOT_FOUND,
        
        [Description("Nhà thầu đã có bảo lãnh hoạt động")]
        CONTRACTOR_HAS_ACTIVATED_GUARANTEE,
        
        [Description("Không tìm thấy thông tin bảo lãnh")]
        GUARANTEE_NOT_FOUND,
        
        [Description("Không được xóa nhà thầu này vì nhà thầu đã có bảo lãnh hoạt động")]
        CANT_DELETE_CONTRACTOR_HAS_GUARANTEE,
        
        [Description("Không được thay đổi nhà thầu")]
        CANT_UPDATE_CONTRACTOR,
        
        [Description("Không được thêm nhiều nhà thầu cùng nhóm")]
        INSERT_MULTIPLE_CONTRACTOR_SAME_GROUP,
        
        [Description("Không thể gửi báo cáo này!")]
        REPORT_CANT_SEND,
        
        [Description("Không được xóa báo cáo đang có công việc đang thực hiện")]
        CANNOT_DELETE_REPORT_INPROGRESS,
        
        [Description("Tháng hoặc năm không hợp lệ")]
        INVALID_MONTH_OR_YEAR,
        
        [Description("Không tìm thấy thông tin báo cáo tháng trước")]
        LAST_REPORT_NOT_FOUND,
        
        [Description("Không được phép chỉnh sửa báo cáo công việc tháng.")]
        CAN_NOT_MODIFY_REPORT,
        
        [Description("Chỉ được xóa các công việc chưa bắt đầu thực hiện. Vui lòng thử lại.")]
        CAN_NOT_DELETE_ACTIVITY_INPROGRESS,
        
        [Description("Tháng này đã có báo cáo đăng ký hoạt động")]
        ACTIVITY_REGISTERED_FOR_THIS_MONTH,
        
        [Description("Chỉ được bắt đầu khi công việc chưa bắt đầu")]
        START_ACTIVITY_INVALID,
        
        [Description("Chỉ được kết thúc khi công việc đang thực hiện")]
        FINISH_ACTIVITY_INVALID,
        
        [Description("Chỉ được cập nhật khi công việc đang thực hiện")]
        UPDATE_ACTIVITY_INVALID,
        
        [Description("Công việc chưa được đăng ký cho báo cáo này")]
        ACTIVITY_NOT_REGISTERED_IN_REPORT
    }
}