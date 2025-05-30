import { Component, Inject, OnInit } from '@angular/core';
import { MatLegacyDialogRef as MatDialogRef, MAT_LEGACY_DIALOG_DATA as MAT_DIALOG_DATA } from '@angular/material/legacy-dialog';
import { InformacoesConfirmacaoDialog } from '../../models/InformacoesConfirmacaoDialog.model';

@Component({
  selector: 'app-confirmacao-dialog',
  templateUrl: './confirmacao-dialog.component.html',
  styleUrls: ['./confirmacao-dialog.component.scss']
})
export class ConfirmacaoDialogComponent implements OnInit {

  constructor(
    public dialogRef: MatDialogRef<ConfirmacaoDialogComponent>,
    @Inject(MAT_DIALOG_DATA) public dados: InformacoesConfirmacaoDialog
  ) { }

  ngOnInit(): void {
  }

  negar(): void {
    this.dialogRef.close();
  }
}
