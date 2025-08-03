import { Component } from '@angular/core';
import Swal from 'sweetalert2';
import { codigoService } from './services/control/codigo.service';
import { BarcodeFormat, BrowserMultiFormatReader } from '@zxing/library';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.scss']
})
export class AppComponent {
  allowedFormats = [BarcodeFormat.QR_CODE];
  mostrarEscaner: boolean = false;
  codigoEscaneado: any = [];
  selectedDevice: MediaDeviceInfo | undefined;
  isData: boolean = false;
  private codeReader: BrowserMultiFormatReader;
  availableDevices: MediaDeviceInfo[] = [];
  torchEnabled = false;

  mostrarScan: boolean = true;
  mostrarCodigo: boolean = false;
  codigoEscaneadoInput: string = '';

  videoConstraints = {
    width: { ideal: 1280 },
    height: { ideal: 720 },
    facingMode: 'environment' // Preferir cámara trasera
  };

  stateOptions: any[] = [
    { label: 'Off', value: 'off' },
    { label: 'On', value: 'on' }
  ];

  constructor(
    private codigoService: codigoService
  ) {
    this.codeReader = new BrowserMultiFormatReader();
  }

  ngOnInit(): void {
    this.codeReader.listVideoInputDevices().then((videoInputDevices) => {
      if (videoInputDevices.length > 0) {
        this.selectedDevice = videoInputDevices[0]
      }
    }).catch((err) => this.mensajeSweetalert2('error', err));
  }

  activarEscaner() {
    this.detenerCamara();
    this.mostrarEscaner = true;
    this.isData = false;
    this.codigoEscaneado = null;
  }

  escaneoExitoso(resultado: string) {
    this.codigoEscaneado = resultado;// Guarda el código QR escaneado
    this.detenerCamara();
    this.alertaOpenLoading();
    this.mostrarEscaner = false;
    this.codigoService.getListarUsuario(resultado).subscribe({
      next: (r) => {
        this.alertaCloseLoading();
        if (r.data.length > 0 && r.respuesta) {
          this.isData = true;
          this.codigoEscaneado = r.data;
          this.mensajeSweetalert2('success', 'El QR o codigo es correcto.');
        } else {
          this.codigoEscaneado = [];
          this.isData = false;
          this.mensajeSweetalert2('error', 'El QR o codigo no es correcto.');
        }
      },
      error: (err) => {
        this.codigoEscaneado = [];
        this.mensajeSweetalert2('error', 'El QR o codigo no es correcto.');
      }
    });
  }

  detenerCamara() {
    this.codeReader.reset();
  }

  mensajeErrorCamera(error: any) {
    if (error.name === 'NotReadableError') {
      this.mostrarEscaner = false;
      this.mensajeSweetalert2('error', 'No se pudo acceder a la cámara. Es posible que esté siendo utilizada por otra aplicación.');
    } else {
      this.mensajeSweetalert2('error', `Error al acceder a la cámara: ${error.message}`);
    }
  }

  onCamerasFound(devices: MediaDeviceInfo[]) {
    this.availableDevices = devices;

    // Buscar cámara trasera
    const backCamera = devices.find(device =>
      device.label.toLowerCase().includes('back') ||
      device.label.toLowerCase().includes('rear') ||
      device.label.toLowerCase().includes('environment')
    );

    this.selectedDevice = backCamera || devices[0]; // fallback si no se encuentra
  }


  onClickDescargarPdf(data: any) {
    const fileUrl = 'assets/pdf/' + data + ' POSOLOGÍA' + '.pdf';
    const fileName = data + ' POSOLOGÍA' + '.pdf';

    const a = document.createElement('a');
    a.href = fileUrl;
    a.download = fileName;
    document.body.appendChild(a);
    a.click();
    document.body.removeChild(a);
  }

  mensajeSweetalert2(icon: any, title: any) {
    const Toast = Swal.mixin({
      toast: true,
      position: "top",
      showConfirmButton: false,
      timer: 5000,
      timerProgressBar: true,
      didOpen: (toast) => {
        toast.onmouseenter = Swal.stopTimer;
        toast.onmouseleave = Swal.resumeTimer;
      }
    });
    Toast.fire({
      icon: icon,
      title: title
    });
  }

  alertaOpenLoading(): any {
    Swal.fire({
      title: 'Obteniendo Datos',
      html: 'Espere un momento por favor',
      allowOutsideClick: false,
      didOpen: () => {
        Swal.showLoading()
      },
    }).then((result) => {
      if (result.dismiss === Swal.DismissReason.timer) {
        console.log('I was closed by the timer')
      }
    });
  }

  alertaCloseLoading(): any {
    Swal.close();
  }

  toggleTorch() {
    this.torchEnabled = !this.torchEnabled;
  }

  onClickMostrarScan(mostrarScan: boolean, mostrarCodigo: boolean) {
    this.mostrarScan = mostrarScan;
    this.mostrarCodigo = mostrarCodigo;
    this.isData = false;
    this.codigoEscaneado = [];
  }

  onClickValidarCodigo() {
    if (this.codigoEscaneadoInput.trim() === '') {
      this.mensajeSweetalert2('error', 'Por favor, ingrese un código válido.');
      return;
    }

    this.escaneoExitoso(this.codigoEscaneadoInput);
    this.codigoEscaneadoInput = ''; // Limpiar el campo de entrada después de la validación
  }

}
